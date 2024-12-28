'*******************************************************************************
'With this program, commands are sent to and received from the connected instrument.
'To send a command, enter the command to be sent in the command field and press the [Send] button.
'For a command with a response (command that includes "?"), a response is displayed in the text field.
'*******************************************************************************

Public Class Form1
    Dim MsgBuf As String = ""                                               'Receiving buffer
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            SerialPort1.PortName = TextBox1.Text                            'Port name setting
            SerialPort1.Open()                                              'Open

            Button1.Enabled = False
            Button2.Enabled = True
            Button3.Enabled = True
            TextBox2.Enabled = True
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            SerialPort1.Close()                                             'Close
            Button1.Enabled = True
            Button2.Enabled = False
            Button3.Enabled = False
            TextBox2.Enabled = False
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

        If InStr(TextBox2.Text, "?") = 0 Then                               'If command does not include "?", only command sending is executed
            SendMsg(TextBox2.Text)                                          'Send message
            TextBox3.Text += "> " + TextBox2.Text + vbCrLf
        Else                                                                'If command includes "?", command sending and response receiving are executed
            SendQueryMsg(TextBox2.Text)                                     'Send message and Receive message
            TextBox3.Text += "> " + TextBox2.Text + vbCrLf
            TextBox3.Text += "< " + MsgBuf
        End If

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        TextBox3.Text = ""                                                  'Clear
    End Sub

    Private Sub SendMsg(ByVal strMsg As String)
        Try
            strMsg = strMsg & vbCrLf
            SerialPort1.WriteLine(strMsg)                                   'Send message
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub

    Private Sub SendQueryMsg(ByVal strMsg As String)
        Try
            SendMsg(strMsg)
            Dim Check As Integer
            MsgBuf = Nothing
            Do                                                              'Wait until response is received
                Check = SerialPort1.ReadByte()
                If Chr(Check) = vbLf Then
                    Exit Do
                ElseIf Chr(Check) = vbCr Then
                Else
                    MsgBuf = MsgBuf & Chr(Check)
                End If
            Loop
            MsgBuf = MsgBuf & vbCrLf
        Catch Ex As Exception
            MsgBuf = vbCrLf
            MsgBox(Ex.Message)
        End Try
    End Sub

End Class


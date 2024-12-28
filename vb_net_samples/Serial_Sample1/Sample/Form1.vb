'*******************************************************************************
'With this program, when the [Measure] button is pressed, 
'the triggering and measurement frequency are set, 
'and then the measured value is acquired after triggering. 
'*******************************************************************************

Public Class Form1
    Dim MsgBuf As String = ""                                   'Receiving buffer

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        'Serial port setting
        SerialPort1.PortName = TextBox1.Text                    'Port name setting
        SerialPort1.BaudRate = 9600                             'Baud rate setting
        SerialPort1.NewLine = vbCrLf                            'Terminator setting
        SerialPort1.Handshake = IO.Ports.Handshake.None         'Handshake setting

        Button1.Enabled = False

        Try
            SerialPort1.Open()                                  'Open

            SendMsg("*RST")                                     'Reset
            SendMsg(":TRIGger EXTernal")                        'Trigger:External
            SendMsg(":FREQuency 1E+06")                         'Frequency:1MHz
            SendMsg("*TRG")                                     'Trigger
            SendQueryMsg(":MEASure?")                           'Acquire measured value

            TextBox2.Text = MsgBuf                              'Display measured value

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        If SerialPort1.IsOpen Then
            Try
                SerialPort1.Close()                             'Close
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If

        Button1.Enabled = True

    End Sub

    Private Sub SendMsg(ByVal strMsg As String)
        Try
            strMsg = strMsg & vbCrLf
            SerialPort1.WriteLine(strMsg)                       'Send message
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub

    Private Sub SendQueryMsg(ByVal strMsg As String)
        Try
            SendMsg(strMsg)
            Dim Check As Integer
            MsgBuf = Nothing
            Do                                                  'Wait until response is received
                Check = SerialPort1.ReadByte()
                If Chr(Check) = vbLf Then
                    Exit Do
                ElseIf Chr(Check) = vbCr Then
                Else
                    MsgBuf = MsgBuf & Chr(Check)
                End If
            Loop
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub
End Class
'*******************************************************************************
'With this program, sweep measurement is executed in the analyzer mode.
'When you enter the sweep setting state and press the [Sweep Measure] button, 
'the result is displayed in the text field.
'Pressing the [Save] button saves the result in a text file.
'*******************************************************************************

Public Class Form1
    Dim MsgBuf As String = ""                                                   'Receiving buffer

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ComboBox1.SelectedIndex = 1
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            SerialPort1.PortName = TextBox1.Text                                'Port name setting
            SerialPort1.Open()                                                  'Open
            Button1.Enabled = False
            Button2.Enabled = False

            SendMsg("*RST")                                                     'Reset
            SendMsg(":MODE ANALyzer")                                           'Mode:Analyzer
            SendMsg(":SWEep:TRIGger SEQuential")                                'Trigger:Sequential
            SendMsg(":LIST:STARt:STOP " + TextBox2.Text + "," _
                                        + TextBox3.Text + "," _
                                        + NumericUpDown1.Value.ToString + "," _
                                        + ComboBox1.SelectedItem)               'Sweep:100Hz-120kHz,101,LOG
            SendMsg(":PARameter1 Z")                                            'Parameter1:Z
            SendMsg(":PARameter2 OFF")                                          'Parameter2:OFF
            SendMsg(":PARameter3 Phase")                                        'Parameter3:Phase
            SendMsg(":PARameter4 OFF")                                          'Parameter4:OFF

            SendMsg("*TRG")                                                     'Trigger
            SendQueryMsg(":MEASure?")                                           'Acquire measured value
            ConvertCSV(MsgBuf)                                                  'Convert to CSV format

            TextBox4.Text = MsgBuf

        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try

        If SerialPort1.IsOpen Then
            SerialPort1.Close()                                                 'Close
        End If
        Button1.Enabled = True
        Button2.Enabled = True

    End Sub

    Private Sub SendMsg(ByVal strMsg As String)
        Try
            strMsg = strMsg & vbCrLf
            SerialPort1.WriteLine(strMsg)                                       'Send message
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub

    Private Sub SendQueryMsg(ByVal strMsg As String)
        Try
            SendMsg(strMsg)
            Dim Check As Integer
            MsgBuf = Nothing
            Do                                                                  'Wait until response is received
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

    Private Sub ConvertCSV(ByRef str As String)
        Dim strary() As String
        Dim pointnum As Integer

        strary = str.Split(",")                                                 'Convert measured value to array
        pointnum = strary.Count / 3                                             'Number of measured points

        str = Nothing
        For index = 0 To pointnum - 1
            str += strary(3 * index + 0) + ","                                  'Frequency
            str += strary(3 * index + 1) + ","                                  'Parameter1 measurement value
            str += strary(3 * index + 2) + vbCrLf                               'Parameter2 measurement value
        Next

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
            Try
                Dim filename As String = SaveFileDialog1.FileName               'Filename setting
                Dim fp As New System.IO.StreamWriter(filename, False, _
                    System.Text.Encoding.GetEncoding("shift_jis"))              'File open
                fp.Write("Frequency,Z,Phase" + vbCrLf)                          'Output header
                fp.Write(TextBox4.Text)                                         'Output data
                fp.Close()                                                      'File close
            Catch Ex As Exception
                MsgBox(Ex.Message)
            End Try
        End If
    End Sub
End Class


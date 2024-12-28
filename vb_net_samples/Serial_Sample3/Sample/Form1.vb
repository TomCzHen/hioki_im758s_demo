'*******************************************************************************
'With this program, open-circuit compensation and short-circuit compensation are executed in the LCR mode.
'Press [Execute open-circuit compensation] or [Execute short-circuit compensation] to acquire the compensation value.
'As it takes time to acquire the compensation value, wait until the compensation is completed 
'while monitoring the CEM bit of the event status resistor.
'*******************************************************************************

Public Class Form1
    Dim MsgBuf As String = ""                                                               'Receiving buffer

    Enum ESE0
        REF = 128
        COF = 64
        LOF = 32
        MOF = 16
        MUF = 8
        IDX = 4
        EOM = 2
        CEM = 1
    End Enum

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ExecAdjust("OPEN")
    End Sub

    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ExecAdjust("SHORT")
    End Sub

    Private Sub ExecAdjust(ByVal mode As String)
        Try
            SerialPort1.PortName = TextBox1.Text                                            'Port name setting
            SerialPort1.Open()                                                              'Open
            Button1.Enabled = False
            Button2.Enabled = False

            SendMsg(":MODE LCR")                                                            'Mode:LCR
            SendMsg(":TRIGger EXTernal")                                                    'Trigger:External
            SendQueryMsg(":ESR0?")                                                          'Check event status resistor
            SendMsg(":CORRection:CALIbration:RETurn ALL")                                   'ALL calibration

            Select Case mode
                Case "OPEN"
                    MsgBox("Open-circuit calibration" + vbCrLf + _
                           "Check that the measurement cable is in an open circuit state.") 'Confirmation message

                    SendMsg(":CORRection:CALIbration:OPEN ACDC")                            'Execute open-circuit calibration

                Case "SHORT"
                    MsgBox("Short-circuit calibration" + vbCrLf + _
                           "Check that the measurement cable is in an open circuit state.") 'Confirmation message

                    SendMsg(":CORRection:CALIbration:SHORt ACDC")                           'Execute short-circuit calibration

            End Select

            Do                                                                              'Wait until compensation is completed
                SendQueryMsg(":ESR0?")                                                      'Check event status resistor
                If MsgBuf And ESE0.CEM Then                                                 'Monitor compensation completion bit
                    Exit Do
                End If
                System.Threading.Thread.Sleep(500)
                System.Windows.Forms.Application.DoEvents()

                If ProgressBar1.Value < ProgressBar1.Maximum Then
                    ProgressBar1.Value += 1                                                 'Progress bar processing
                End If
            Loop

            ProgressBar1.Value = ProgressBar1.Maximum

            SendQueryMsg(":CORRection:ERRor?")

            Select Case MsgBuf
                Case "0"
                    MsgBox("Calibration ended normally.")

                Case "1"
                    MsgBox("Calibration ended abnormally.")

                Case "2"
                    MsgBox("Calibration stopped.")

            End Select

            ProgressBar1.Value = 0

        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try

        If SerialPort1.IsOpen Then
            Try
                SerialPort1.Close()                                                         'Close
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
        Button1.Enabled = True
        Button2.Enabled = True
    End Sub

    Private Sub SendMsg(ByVal strMsg As String)
        Try
            strMsg = strMsg & vbCrLf
            SerialPort1.WriteLine(strMsg)                                                   'Send message
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub

    Private Sub SendQueryMsg(ByVal strMsg As String)
        Try
            SendMsg(strMsg)
            Dim Check As Integer
            MsgBuf = Nothing
            Do                                                                              'Wait until response is received
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


'*******************************************************************************
'With this program, comparator measurement is executed in the LCR mode 
'and the measured value and judgment result are displayed.
'Set the upper and lower limit values and press [Measure].
'*******************************************************************************

Public Class Form1
    Dim MsgBuf As String = ""                                                               'Receiving buffer
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim strary() As String

        Try
            SerialPort1.PortName = TextBox1.Text                                            'Port name setting
            SerialPort1.Open()                                                              'Open
            GroupBox1.Enabled = False

            SendMsg(":MODE LCR")                                                            'Mode LCR
            SendMsg(":TRIGger EXTernal")                                                    'Trigger:External

            SendMsg(":COMParator ON")                                                       'Judge:ON
            SendMsg(":COMParator:FLIMit:ABSolute " + TextBox3.Text + "," + TextBox2.Text)   'Z: HI and LO setting
            SendMsg(":COMParator:SLIMit:ABSolute " + TextBox5.Text + "," + TextBox4.Text)   'Phase: HI and LO setting

            SendMsg("*TRG")                                                                 'Trigger

            SendQueryMsg(":MEASure?")                                                       'Acquire measured value and judge result

            MsgBuf = MsgBuf.Replace(vbCrLf, "")                                             'Remove line feed
            strary = MsgBuf.Split(",")                                                      'Device measured value by comma

            Label10.ForeColor = GetColorResultAll(strary(0))
            Label10.Text = GetStrResultAll(strary(0))

            TextBox6.Text = strary(1)

            Label8.ForeColor = GetColorResult(strary(2))
            Label8.Text = GetStrResult(strary(2))

            TextBox7.Text = strary(3)

            Label9.ForeColor = GetColorResult(strary(4))
            Label9.Text = GetStrResult(strary(4))

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

        GroupBox1.Enabled = True
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

    Private Function GetStrResultAll(ByVal str As String) As String
        Dim resultAll() As String = {"NG", "IN"}                                            'Logical product of comparison results
        Dim ret As String = ""
        Select Case str
            Case "0"
                ret = resultAll(0)
            Case "1"
                ret = resultAll(1)
        End Select
        GetStrResultAll = ret
    End Function

    Private Function GetStrResult(ByVal str As String) As String
        Dim result() As String = {"LO", "IN", "HI", "-"}                                    'Comparison results
        Dim ret As String = ""
        Select Case str
            Case "-1"
                ret = result(0)
            Case "0"
                ret = result(1)
            Case "1"
                ret = result(2)
            Case "2"
                ret = result(3)
        End Select
        GetStrResult = ret
    End Function

    Private Function GetColorResultAll(ByVal str As String) As Color
        Dim ret As Color = Color.Black
        Select Case str
            Case "0"
                ret = Color.Red
            Case "1"
                ret = Color.LimeGreen
        End Select
        GetColorResultAll = ret
    End Function

    Private Function GetColorResult(ByVal str As String) As Color
        Dim ret As Color = Color.Black
        Select Case str
            Case "-1"
                ret = Color.Red
            Case "0"
                ret = Color.LimeGreen
            Case "1"
                ret = Color.Red
            Case "2"
                ret = Color.Black
        End Select
        GetColorResult = ret
    End Function

End Class


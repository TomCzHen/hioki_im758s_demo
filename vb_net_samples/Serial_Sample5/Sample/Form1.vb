'*******************************************************************************
'With this program, when the [Measure] button is pressed, the instrument is changed to 
'the binary communication mode and the measured value is acquired after triggering.
'The received binary data are then converted to the ASCII character string.
'*******************************************************************************

Public Class Form1
    Dim MsgBufbin() As Byte                                                         'Receiving buffer(Byte type array)

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim val1(3) As Byte
        Dim val1Single As Single
        Dim val2(3) As Byte
        Dim val2Single As Single

        Button1.Enabled = False

        Try
            'Serial port setting
            SerialPort1.PortName = TextBox1.Text                                    'Port name setting
            SerialPort1.Open()                                                      'Open

            SendMsg("*RST")                                                         'Reset
            SendMsg(":TRIGger EXTernal")                                            'Trigger:External
            SendMsg(":FORMat:DATA REAL")                                            'Data format:Binary
            SendMsg("*TRG")                                                         'Trigger
            SendQueryMsgBin(":MEASure?")                                            'Acquire binary measured value

            Array.Copy(MsgBufbin, 0, val1, 0, 4)                                    'Measurement value1
            Array.Copy(MsgBufbin, 4, val2, 0, 4)                                    'Measurement value2

            val1Single = ConvertBin2Single(val1)                                    'Convert to value
            val2Single = ConvertBin2Single(val2)

            TextBox2.Text = BitConverter.ToString(MsgBufbin)
            TextBox3.Text = val1Single.ToString
            TextBox4.Text = val2Single.ToString

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        If SerialPort1.IsOpen Then
            Try
                SerialPort1.Close()                                                 'Close
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If

        Button1.Enabled = True

    End Sub

    Private Sub SendMsg(ByVal strMsg As String)
        Try
            strMsg = strMsg & vbCrLf
            SerialPort1.WriteLine(strMsg)                                           'Send message
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub

    Private Sub SendQueryMsgBin(ByVal strMsg As String)
        Try
            SendMsg(strMsg)
            Dim Check As Integer
            Dim Checkstr As String
            Dim digit As Integer
            Dim rcvsize As Integer = 0

            Check = SerialPort1.ReadByte()                                          'Receive #
            If Not Chr(Check) = "#" Then
                Exit Sub
            End If

            Check = SerialPort1.ReadByte()                                          'Receive number of receiving byte digits
            Checkstr = Chr(Check)                                                   'Convert character code to character
            digit = Convert.ToInt32(Checkstr)                                       'Convert character to value

            For index = 1 To digit
                Check = SerialPort1.ReadByte()                                      'Receive number of receiving bytes
                Checkstr = Chr(Check)                                               'Convert character code to character
                rcvsize += Convert.ToInt32(Checkstr) * Math.Pow(10, (index - 1))    'Binary size to be acquired
            Next

            MsgBufbin = Nothing
            ReDim MsgBufbin(rcvsize - 1)
            For index = 1 To rcvsize
                Check = SerialPort1.ReadByte()
                MsgBufbin(index - 1) = BitConverter.GetBytes(Check)(0)              'Receive data
            Next

            Do                                                                      'Receive terminator
                Check = SerialPort1.ReadByte()
                If Chr(Check) = vbLf Then
                    Exit Do
                End If
            Loop

        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub

    Private Function ConvertBin2Single(ByVal val() As Byte) As Single
        Dim valSingle As Single

        Array.Reverse(val)                                                          'Convert to little endian
        valSingle = BitConverter.ToSingle(val, 0)                                   'Convert byte string

        ConvertBin2Single = valSingle
    End Function

End Class
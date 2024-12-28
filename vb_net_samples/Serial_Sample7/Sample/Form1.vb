'*******************************************************************************
'This program can be saved in BMP format to the measurement screen.
'*******************************************************************************

Public Class Form1
    Dim rcv_buf(4000000) As Byte                                                'Receiving buffer

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim rcv_size As Integer = 0
            Dim recvfilesize As Integer
            Dim cmd As String

            SerialPort1.PortName = TextBox1.Text                                'Port name setting
            SerialPort1.Open()                                                  'Open
            Button1.Enabled = False
            GroupBox1.Enabled = False

            SerialPort1.DiscardOutBuffer()
            SerialPort1.DiscardInBuffer()

            '----------------------------------------
            'Save file dialog
            '----------------------------------------
            SaveFileDialog1.Filter = "BMP (*.bmp)|*.bmp"
            SaveFileDialog1.FileName = Tbx_filename.Text
            SaveFileDialog1.InitialDirectory = Environment.SpecialFolder.DesktopDirectory

            If SaveFileDialog1.ShowDialog() = DialogResult.OK Then

                If Rbtn_color.Checked Then
                    cmd = ":HCOPy:DATA? COLor"
                Else
                    cmd = ":HCOPy:DATA? MONochrome"
                End If

                SerialPort1.Write(cmd + vbCrLf)                                 'Send message

                '----------------------------------------
                'Check receiving BMP size
                '----------------------------------------
                Dim keta As Integer
                Dim rcv_totalsize As Integer = 0
                Dim recvfilesize_str As String = ""

                rcv_buf(0) = SerialPort1.ReadByte()                             'Receive #
                rcv_buf(0) = SerialPort1.ReadByte()                             'Receive number of receiving byte digits
                keta = Val(Convert.ToChar(rcv_buf(0)))

                For index = 1 To 7
                    rcv_buf(0) = SerialPort1.ReadByte()                         'Receive number of receiving bytes
                    recvfilesize_str += Convert.ToChar(rcv_buf(0))              'Convert byte string to character string
                Next
                recvfilesize = Val(recvfilesize_str) + 2                        'Add terminator byte

                '----------------------------------------
                'Progress bar setting
                '----------------------------------------
                ProgressBar1.Maximum = recvfilesize
                ProgressBar1.Value = 0

                '----------------------------------------
                'Receive BMP data
                '----------------------------------------
                Do
                    If SerialPort1.BytesToRead > 0 Then
                        rcv_size = SerialPort1.Read(rcv_buf, rcv_totalsize, 1000) 'Acquire receiving buffer
                        rcv_totalsize += rcv_size
                    End If
                    ProgressBar1.Value = rcv_totalsize
                    If rcv_totalsize = recvfilesize Then
                        Exit Do
                    End If
                Loop

                '----------------------------------------
                'Write file
                '----------------------------------------
                Dim fs As New System.IO.FileStream(SaveFileDialog1.FileName, System.IO.FileMode.Create, System.IO.FileAccess.Write)
                Try
                    fs.Write(rcv_buf, 0, recvfilesize - 2)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

                fs.Close()

                MsgBox("The picture was acquired.")
                ProgressBar1.Value = 0
            End If

        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try

        If SerialPort1.IsOpen Then
            SerialPort1.Close()                                                 'Close
        End If
        Button1.Enabled = True
        GroupBox1.Enabled = True

    End Sub
End Class


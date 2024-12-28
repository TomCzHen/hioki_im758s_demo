<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Tbx_filename = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SerialPort1 = New System.IO.Ports.SerialPort(Me.components)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.Tpage_IM3533 = New System.Windows.Forms.TabPage()
        Me.Rbtn_mono = New System.Windows.Forms.RadioButton()
        Me.Rbtn_color = New System.Windows.Forms.RadioButton()
        Me.GroupBox1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.Tpage_IM3533.SuspendLayout()
        Me.SuspendLayout()
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(12, 222)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(268, 20)
        Me.ProgressBar1.TabIndex = 33
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(223, 101)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(28, 12)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = ".bmp"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TabControl1)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Tbx_filename)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 29)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(268, 136)
        Me.GroupBox1.TabIndex = 32
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Set"
        '
        'Tbx_filename
        '
        Me.Tbx_filename.Location = New System.Drawing.Point(78, 98)
        Me.Tbx_filename.Name = "Tbx_filename"
        Me.Tbx_filename.Size = New System.Drawing.Size(139, 19)
        Me.Tbx_filename.TabIndex = 5
        Me.Tbx_filename.Text = "Image"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(21, 101)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 12)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Filename"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(65, 171)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(163, 46)
        Me.Button1.TabIndex = 30
        Me.Button1.Text = "Save"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(26, 12)
        Me.Label1.TabIndex = 29
        Me.Label1.Text = "Port"
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.FileName = "Data"
        Me.SaveFileDialog1.Filter = "CSVファイル(*.csv)|*.csv"
        Me.SaveFileDialog1.RestoreDirectory = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(58, 4)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(66, 19)
        Me.TextBox1.TabIndex = 28
        Me.TextBox1.Text = "COM1"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.Tpage_IM3533)
        Me.TabControl1.Location = New System.Drawing.Point(30, 19)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(196, 65)
        Me.TabControl1.TabIndex = 18
        '
        'Tpage_IM3533
        '
        Me.Tpage_IM3533.Controls.Add(Me.Rbtn_mono)
        Me.Tpage_IM3533.Controls.Add(Me.Rbtn_color)
        Me.Tpage_IM3533.Location = New System.Drawing.Point(4, 22)
        Me.Tpage_IM3533.Name = "Tpage_IM3533"
        Me.Tpage_IM3533.Padding = New System.Windows.Forms.Padding(3)
        Me.Tpage_IM3533.Size = New System.Drawing.Size(188, 39)
        Me.Tpage_IM3533.TabIndex = 1
        Me.Tpage_IM3533.Text = "IM7580"
        Me.Tpage_IM3533.UseVisualStyleBackColor = True
        '
        'Rbtn_mono
        '
        Me.Rbtn_mono.AutoSize = True
        Me.Rbtn_mono.Location = New System.Drawing.Point(88, 14)
        Me.Rbtn_mono.Name = "Rbtn_mono"
        Me.Rbtn_mono.Size = New System.Drawing.Size(87, 16)
        Me.Rbtn_mono.TabIndex = 23
        Me.Rbtn_mono.TabStop = True
        Me.Rbtn_mono.Text = "Monochrome"
        Me.Rbtn_mono.UseVisualStyleBackColor = True
        '
        'Rbtn_color
        '
        Me.Rbtn_color.AutoSize = True
        Me.Rbtn_color.Checked = True
        Me.Rbtn_color.Location = New System.Drawing.Point(15, 14)
        Me.Rbtn_color.Name = "Rbtn_color"
        Me.Rbtn_color.Size = New System.Drawing.Size(50, 16)
        Me.Rbtn_color.TabIndex = 22
        Me.Rbtn_color.TabStop = True
        Me.Rbtn_color.Text = "Color"
        Me.Rbtn_color.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(292, 246)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBox1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.Tpage_IM3533.ResumeLayout(False)
        Me.Tpage_IM3533.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Tbx_filename As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents SerialPort1 As System.IO.Ports.SerialPort
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents Tpage_IM3533 As System.Windows.Forms.TabPage
    Friend WithEvents Rbtn_mono As System.Windows.Forms.RadioButton
    Friend WithEvents Rbtn_color As System.Windows.Forms.RadioButton

End Class

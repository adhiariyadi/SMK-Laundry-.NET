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
        Me.Logo = New System.Windows.Forms.Label()
        Me.username = New System.Windows.Forms.Label()
        Me.password = New System.Windows.Forms.Label()
        Me.inputUsername = New System.Windows.Forms.TextBox()
        Me.inputPassword = New System.Windows.Forms.TextBox()
        Me.reset = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Logo
        '
        Me.Logo.AutoSize = True
        Me.Logo.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Logo.Location = New System.Drawing.Point(107, 40)
        Me.Logo.Name = "Logo"
        Me.Logo.Size = New System.Drawing.Size(187, 25)
        Me.Logo.TabIndex = 1
        Me.Logo.Text = "Esemka Laundry"
        '
        'username
        '
        Me.username.AutoSize = True
        Me.username.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.username.Location = New System.Drawing.Point(60, 91)
        Me.username.Name = "username"
        Me.username.Size = New System.Drawing.Size(83, 20)
        Me.username.TabIndex = 2
        Me.username.Text = "Username"
        '
        'password
        '
        Me.password.AutoSize = True
        Me.password.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.password.Location = New System.Drawing.Point(60, 135)
        Me.password.Name = "password"
        Me.password.Size = New System.Drawing.Size(78, 20)
        Me.password.TabIndex = 3
        Me.password.Text = "Password"
        '
        'inputUsername
        '
        Me.inputUsername.Location = New System.Drawing.Point(159, 93)
        Me.inputUsername.Name = "inputUsername"
        Me.inputUsername.Size = New System.Drawing.Size(180, 20)
        Me.inputUsername.TabIndex = 4
        '
        'inputPassword
        '
        Me.inputPassword.Location = New System.Drawing.Point(159, 135)
        Me.inputPassword.Name = "inputPassword"
        Me.inputPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.inputPassword.Size = New System.Drawing.Size(180, 20)
        Me.inputPassword.TabIndex = 5
        '
        'reset
        '
        Me.reset.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.reset.Location = New System.Drawing.Point(130, 177)
        Me.reset.Name = "reset"
        Me.reset.Size = New System.Drawing.Size(99, 29)
        Me.reset.TabIndex = 6
        Me.reset.Text = "Reset"
        Me.reset.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(240, 177)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(99, 29)
        Me.Button2.TabIndex = 7
        Me.Button2.Text = "Login"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(408, 262)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.reset)
        Me.Controls.Add(Me.inputPassword)
        Me.Controls.Add(Me.inputUsername)
        Me.Controls.Add(Me.password)
        Me.Controls.Add(Me.username)
        Me.Controls.Add(Me.Logo)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Logo As System.Windows.Forms.Label
    Friend WithEvents username As System.Windows.Forms.Label
    Friend WithEvents password As System.Windows.Forms.Label
    Friend WithEvents inputUsername As System.Windows.Forms.TextBox
    Friend WithEvents inputPassword As System.Windows.Forms.TextBox
    Friend WithEvents reset As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button

End Class

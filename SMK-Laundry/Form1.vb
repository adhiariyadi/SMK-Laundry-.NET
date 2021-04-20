Public Class Form1
    Dim query As String
    Public nama, userId As String

    Private Sub reset_Click(sender As Object, e As EventArgs) Handles reset.Click
        inputUsername.Text = ""
        inputPassword.Text = ""
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        query = "select * from Employee where email='" & inputUsername.Text & "' and password='" & inputPassword.Text & "'"
        If read(query).Rows.Count > 0 Then
            nama = read(query).Rows(0)("name")
            userId = read(query).Rows(0)("id")
            MsgBox("Selamat Datang " + nama, MsgBoxStyle.Information, "info")
            MainForm.Show()
            MainForm.Label2.Text = "Hello, " + nama
            ManageEmployee.Label2.Text = "Hello, " + nama
            ManageService.Label2.Text = "Hello, " + nama
            ManagePackage.Label2.Text = "Hello, " + nama
            TransactionDeposit.Label2.Text = "Hello, " + nama
            TransactionDeposit.employeeId.Text = userId
            PrepaidPackage.Label2.Text = "Hello, " + nama
            ViewTransaction.Label2.Text = "Hello, " + nama
            Customer.Label2.Text = "Hello, " + nama
            Me.Hide()
        Else
            MsgBox("Login Gagal" & " username atau password salah", MsgBoxStyle.Critical, "info")
        End If
    End Sub
End Class

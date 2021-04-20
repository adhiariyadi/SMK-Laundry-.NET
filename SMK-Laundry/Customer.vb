Public Class Customer
    Dim query As String
    Sub kosongkanData()
        customerName.Text = ""
        phoneNumber.Text = ""
        address.Text = ""
    End Sub

    Private Sub phoneNumber_KeyPress(sender As Object, e As KeyPressEventArgs) Handles phoneNumber.KeyPress
        Dim kunci As Short = Asc(e.KeyChar)
        If (e.KeyChar Like "[0-9]" _
            OrElse kunci = Keys.Back) Then
            kunci = 0
        End If
        e.Handled = CBool(kunci)
    End Sub

    Private Sub Customer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call kosongkanData()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If phoneNumber.Text = "" Or customerName.Text = "" Or address.Text = "" Then
            MsgBox("Mohon Isi data dengan lengkap")
        Else
            query = "insert into Customer(name,phonenumber,address) values('{0}','{1}','{2}')"
            query = String.Format(query, customerName.Text, phoneNumber.Text, address.Text)
            MsgBox("Insert Data CUstomer Berhasil ", MsgBoxStyle.Information, "Information")
            aksi(query)
            Call kosongkanData()
            Me.Close()
        End If
    End Sub
End Class
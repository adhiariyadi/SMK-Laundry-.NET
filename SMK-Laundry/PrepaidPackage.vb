Imports System.Data.SqlClient
Public Class PrepaidPackage
    Dim conn As SqlConnection
    Dim dr As SqlDataReader
    Dim cmd As SqlCommand
    Dim query, idpackage, idcustomer, idprepaid As String

    Sub koneksi()
        conn = New SqlConnection("Data Source=DESKTOP-88BF2P2;Initial Catalog=laundry;Integrated Security=True")
        If conn.State = ConnectionState.Closed Then conn.Open()
    End Sub

    Sub kosongkanData()
        detailDepositId.Text = ""
        price.Text = ""
    End Sub

    Sub unit()
        query = "select distinct b.name from Package a, Service b where a.idservice=b.id"
        selectPackage.DataSource = read(query)
        selectPackage.DisplayMember = "name"
        selectPackage.ValueMember = "name"
    End Sub

    Sub kondisiAwal()
        query = "select a.id 'DetailDeposit ID',d.name 'Customer',b.name 'Service',a.priceunit,a.totalunit,a.priceunit*a.totalunit 'SUM Total' from DetailDeposit a,Service b,HeaderDeposit c,Customer d where a.idservice=b.id and a.iddeposit=c.id and c.idcustomer=d.id"
        dgv.DataSource = read(query)
        Dim btn As New DataGridViewButtonColumn()
        btn.HeaderText = "Action"
        btn.Text = "Select"
        btn.Name = "btnSelect"
        btn.UseColumnTextForButtonValue = True
        dgv.Columns.Add(btn)
        Call kosongkanData()
    End Sub

    Private Sub selectPackage_SelectedIndexChanged(sender As Object, e As EventArgs) Handles selectPackage.SelectedIndexChanged
        Call koneksi()
        query = "select a.id from package a,service b where name ='" & selectPackage.Text & "' and a.idservice=b.id"
        cmd = New SqlCommand(query, conn)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            idpackage = dr.Item("id")
        End If
    End Sub

    Private Sub PrepaidPackage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call kondisiAwal()
        Call unit()
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        Customer.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        MainForm.Show()
        Me.Hide()
    End Sub

    Private Sub price_KeyPress(sender As Object, e As KeyPressEventArgs) Handles price.KeyPress
        Dim kunci As Short = Asc(e.KeyChar)
        If (e.KeyChar Like "[0-9]" _
            OrElse kunci = Keys.Back) Then
            kunci = 0
        End If
        e.Handled = CBool(kunci)
    End Sub

    Private Sub phoneNumber_TextChanged(sender As Object, e As EventArgs) Handles phoneNumber.TextChanged

    End Sub

    Private Sub dgv_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellContentClick
        detailDepositId.Text = dgv.CurrentRow.Cells(0).Value
        price.Text = dgv.CurrentRow.Cells(5).Value
    End Sub

    Private Sub btnSubmit_Click_1(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If price.Text = "" Or idcustomer = "" Or detailDepositId.Text = "" Then
            MsgBox("Masukkan Data dengan lengkap")
        Else
            query = "insert into PrepaidPackage(idcustomer,idpackage,price,startdatetime) values('{0}','{1}','{2}','{3}')"
            query = String.Format(query, idcustomer, idpackage, price.Text, DateTimePicker1.Value.ToString("yyyy-MM-dd"))
            aksi(query)
            Call koneksi()
            query = "select top (1) * from PrepaidPackage order by id desc"
            cmd = New SqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                idprepaid = dr.Item("id")
            End If
            query = "update DetailDeposit set idprepaidpackage='" & idprepaid & "' where id='" & detailDepositId.Text & "'"
            aksi(query)
            MsgBox("Insert Data PrepaidPackage Berhasil ", MsgBoxStyle.Information, "Information")
            Call kondisiAwal()
        End If
    End Sub

    Private Sub phoneNumber_KeyPress(sender As Object, e As KeyPressEventArgs) Handles phoneNumber.KeyPress
        If e.KeyChar = Chr(13) Then
            Call koneksi()
            query = "select * from customer where phonenumber ='" & phoneNumber.Text & "'"
            cmd = New SqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                idcustomer = dr.Item("id")
                labelName.Text = "[ " + dr.Item("name") + " ]"
                labelAddress.Text = "[ " + dr.Item("address") + " ]"
            End If
        End If
    End Sub
End Class
Imports System.Data.SqlClient
Public Class TransactionDeposit
    Dim conn As SqlConnection
    Dim dr As SqlDataReader
    Dim cmd As SqlCommand
    Dim query, idcustomer, idservice, iddeposit As String

    Sub koneksi()
        conn = New SqlConnection("Data Source=DESKTOP-PQDG5SB;Initial Catalog=laundry;Integrated Security=True")
        If conn.State = ConnectionState.Closed Then conn.Open()
    End Sub

    Sub kosongkanData()
        price.Text = ""
        totalUnit.Text = ""
        phoneNumber.Text = ""
        labelName.Text = "Name"
        labelAddress.Text = "Address"
    End Sub

    Sub service()
        query = "select * from Service order by name"
        selectService.DataSource = read(query)
        selectService.DisplayMember = "name"
        selectService.ValueMember = "name"
    End Sub

    Sub kondisiAwal()
        query = "select a.iddeposit, a.idservice, a.priceunit, a.totalunit, b.idcustomer, b.idemployee, b.transactiondatetime from DetailDeposit a, HeaderDeposit b where a.iddeposit=b.id"
        dgv.DataSource = read(query)
    End Sub

    Private Sub TransactionDeposit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call kondisiAwal()
        Call service()
    End Sub

    Private Sub selectService_SelectedIndexChanged(sender As Object, e As EventArgs) Handles selectService.SelectedIndexChanged
        Call koneksi()
        query = "select * from Service where name='" & selectService.Text & "'"
        cmd = New SqlCommand(query, conn)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            idservice = dr.Item("id")
            price.Text = dr.Item("priceunit")
        End If
    End Sub

    Private Sub totalUnit_KeyPress(sender As Object, e As KeyPressEventArgs) Handles totalUnit.KeyPress
        Dim kunci As Short = Asc(e.KeyChar)
        If (e.KeyChar Like "[0-9]" _
            OrElse kunci = Keys.Back) Then
            kunci = 0
        End If
        e.Handled = CBool(kunci)
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If price.Text = "" Or idcustomer = "" Then
            MsgBox("Masukkan Data dengan lengkap")
        Else
            query = "insert into HeaderDeposit(IdCustomer,IdEmployee,TransactionDatetime) values('{0}','{1}','{2}')"
            query = String.Format(query, idcustomer, employeeId.Text, DateTimePicker1.Value.ToString("yyyy-MM-dd"))
            aksi(query)
            Call koneksi()
            query = "select top (1) * from HeaderDeposit order by id desc"
            cmd = New SqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                iddeposit = dr.Item("id")
            End If
            query = "insert into DetailDeposit(IdDeposit,IdService,IdPrepaidPackage,PriceUnit,TotalUnit) values('{0}','{1}',' ','{2}','{3}')"
            query = String.Format(query, iddeposit, idservice, price.Text, totalUnit.Text)
            aksi(query)
            query = "insert into Package(IdService,TotalUnit,Price) values('{0}','{1}','{2}')"
            query = String.Format(query, idservice, totalUnit.Text, (price.Text * totalUnit.Text))
            aksi(query)
            MsgBox("Insert Data Deposit Berhasil ", MsgBoxStyle.Information, "Information")
            Call kondisiAwal()
        End If
    End Sub

    Private Sub phoneNumber_TextChanged(sender As Object, e As EventArgs) Handles phoneNumber.TextChanged

    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        Customer.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        MainForm.Show()
        Me.Hide()
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
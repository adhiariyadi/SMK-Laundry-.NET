Imports System.Data.SqlClient
Public Class ManagePackage
    Dim conn As SqlConnection
    Dim dr As SqlDataReader
    Dim cmd As SqlCommand
    Dim query, idservice As String

    Sub koneksi()
        conn = New SqlConnection("Data Source=DESKTOP-88BF2P2;Initial Catalog=laundry;Integrated Security=True")
        If conn.State = ConnectionState.Closed Then conn.Open()
    End Sub

    Sub kosongkanData()
        packageId.Text = ""
        selectService.Text = ""
        totalUnit.Text = ""
        price.Text = ""
    End Sub

    Sub addButton()
        Dim btnEdit, btnDelete As New DataGridViewButtonColumn()
        btnEdit.HeaderText = "Edit"
        btnEdit.Text = "Edit"
        btnEdit.Name = "Edit"
        btnEdit.FlatStyle = FlatStyle.Flat
        btnEdit.UseColumnTextForButtonValue = True
        dgv.Columns.Add(btnEdit)
        btnDelete.HeaderText = "Delete"
        btnDelete.Text = "Delete"
        btnDelete.Name = "Delete"
        btnDelete.FlatStyle = FlatStyle.Flat
        btnDelete.UseColumnTextForButtonValue = True
        dgv.Columns.Add(btnDelete)
    End Sub

    Sub removeButton()
        dgv.Columns.RemoveAt(5)
        dgv.Columns.RemoveAt(4)
    End Sub

    Sub kondisiAwal()
        query = "select a.id 'Package ID',b.name,a.totalunit,a.price from Package a,Service b where a.idservice=b.id"
        dgv.DataSource = read(query)
        Call addButton()
        Call kosongkanData()
    End Sub

    Sub service()
        query = "select * from Service order by name"
        selectService.DataSource = read(query)
        selectService.ValueMember = "name"
        selectService.DisplayMember = "name"
    End Sub

    Private Sub ManagePackage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Sub price_KeyPress(sender As Object, e As KeyPressEventArgs) Handles price.KeyPress
        Dim kunci As Short = Asc(e.KeyChar)
        If (e.KeyChar Like "[0-9]" _
            OrElse kunci = Keys.Back) Then
            kunci = 0
        End If
        e.Handled = CBool(kunci)
    End Sub

    Private Sub dgv_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellContentClick
        If e.ColumnIndex = 4 Then
            packageId.Text = dgv.CurrentRow.Cells(0).Value
            selectService.Text = dgv.CurrentRow.Cells(1).Value
            totalUnit.Text = dgv.CurrentRow.Cells(2).Value
            price.Text = dgv.CurrentRow.Cells(3).Value
        ElseIf e.ColumnIndex = 5 Then
            packageId.Text = dgv.CurrentRow.Cells(0).Value
            If MessageBox.Show("Yakin mau hapus?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
                query = "delete from Package where id='" & packageId.Text & "'"
                aksi(query)
                MsgBox("Delete Data Package Berhasil!", MsgBoxStyle.Information, "Information")
                Call removeButton()
                Call kondisiAwal()
            Else
                MsgBox("Delete Data Package Gagal!", MsgBoxStyle.Information, "Information")
                Call removeButton()
                Call kondisiAwal()
            End If
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        If TextBox1.Text = "" Then
            MsgBox("Masukkan Nama Untuk Mencari Data")
        Else
            query = "select a.id 'Employee ID',a.name,a.email,a.address,a.phonenumber,a.dateofbirth,b.name,a.salary from Employee a,Job b where a.idjob=b.id and a.name like '%" & TextBox1.Text & "%'"
            dgv.DataSource = read(query)
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If price.Text = "" Or totalUnit.Text = "" Then
            MsgBox("Mohon isi data dengan lengkap!", MsgBoxStyle.Critical, "Error")
        Else
            If packageId.Text = "" Then
                query = "insert into Package(idservice,totalunit,price) values('{0}','{1}','{2}')"
                query = String.Format(query, idservice, totalUnit.Text, price.Text)
                aksi(query)
                MsgBox("Insert Data Package Berhasil!", MsgBoxStyle.Information, "Information")
                Call removeButton()
                Call kondisiAwal()
            Else
                query = "update Package set idservice='" & idservice & "',totalunit='" & totalUnit.Text & "',price='" & price.Text & "' where id='" & packageId.Text & "'"
                aksi(query)
                MsgBox("Update Data Package Berhasil!", MsgBoxStyle.Information, "Information")
                Call removeButton()
                Call kondisiAwal()
            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        MainForm.Show()
        Me.Hide()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Call kosongkanData()
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            Call removeButton()
            query = "select a.id 'Package ID',b.name,a.totalunit,a.price from Package a,Service b where a.idservice=b.id and b.name like '%" & TextBox1.Text & "%'"
            dgv.AutoGenerateColumns = True
            dgv.DataSource = read(query)
            Call addButton()
        End If
    End Sub
End Class
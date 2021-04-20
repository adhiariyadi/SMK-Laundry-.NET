Imports System.Data.SqlClient
Public Class ManageService
    Dim conn As SqlConnection
    Dim dr As SqlDataReader
    Dim cmd As SqlCommand
    Dim query, idunit, idcategory As String

    Sub koneksi()
        conn = New SqlConnection("Data Source=DESKTOP-88BF2P2;Initial Catalog=laundry;Integrated Security=True")
        If conn.State = ConnectionState.Closed Then conn.Open()
    End Sub

    Sub kosongkanData()
        serviceId.Text = ""
        serviceName.Text = ""
        selectCategory.Text = ""
        selectUnit.Text = ""
        price.Text = ""
        estDuration.Text = ""
    End Sub

    Sub unit()
        query = "select * from Unit order by name"
        selectUnit.DataSource = read(query)
        selectUnit.ValueMember = "name"
        selectUnit.DisplayMember = "name"
    End Sub

    Sub category()
        query = "select * from Category order by name"
        selectCategory.DataSource = read(query)
        selectCategory.ValueMember = "name"
        selectCategory.DisplayMember = "name"
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
        dgv.Columns.RemoveAt(7)
        dgv.Columns.RemoveAt(6)
    End Sub

    Sub kondisiAwal()
        query = "select a.id 'Service ID',a.name,b.name,c.name,a.priceunit,a.estimationDuration from Service a,Category b,Unit c where a.idcategory=b.id and a.idunit=c.id"
        dgv.DataSource = read(query)
        Call addButton()
        Call kosongkanData()
    End Sub

    Private Sub ManageService_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call kondisiawal()
        Call category()
        Call unit()
    End Sub

    Private Sub selectCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles selectCategory.SelectedIndexChanged
        Call koneksi()
        query = "select * from Category where name='" & selectCategory.Text & "'"
        cmd = New SqlCommand(query, conn)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            idcategory = dr.Item("id")
        End If
    End Sub

    Private Sub selectUnit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles selectUnit.SelectedIndexChanged
        Call koneksi()
        query = "select * from Unit where name='" & selectUnit.Text & "'"
        cmd = New SqlCommand(query, conn)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            idunit = dr.Item("id")
        End If
    End Sub

    Private Sub price_KeyPress(sender As Object, e As KeyPressEventArgs) Handles price.KeyPress
        Dim kunci As Short = Asc(e.KeyChar)
        If (e.KeyChar Like "[0-9]" _
            OrElse kunci = Keys.Back) Then
            kunci = 0
        End If
        e.Handled = CBool(kunci)
    End Sub

    Private Sub estDuration_KeyPress(sender As Object, e As KeyPressEventArgs) Handles estDuration.KeyPress
        Dim kunci As Short = Asc(e.KeyChar)
        If (e.KeyChar Like "[0-9]" _
            OrElse kunci = Keys.Back) Then
            kunci = 0
        End If
        e.Handled = CBool(kunci)
    End Sub

    Private Sub dgv_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellContentClick
        If e.ColumnIndex = 6 Then
            serviceId.Text = dgv.CurrentRow.Cells(0).Value
            serviceName.Text = dgv.CurrentRow.Cells(1).Value
            selectCategory.Text = dgv.CurrentRow.Cells(2).Value
            selectUnit.Text = dgv.CurrentRow.Cells(3).Value
            price.Text = dgv.CurrentRow.Cells(4).Value
            estDuration.Text = dgv.CurrentRow.Cells(5).Value
        ElseIf e.ColumnIndex = 7 Then
            serviceId.Text = dgv.CurrentRow.Cells(0).Value
            If MessageBox.Show("Yakin mau hapus?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
                query = "delete Service where id='" & serviceId.Text & "'"
                aksi(query)
                MsgBox("Delete Data Service Berhasil!", MsgBoxStyle.Information, "Information")
                Call removeButton()
                Call kondisiAwal()
            Else
                MsgBox("Delete Data Service Gagal!", MsgBoxStyle.Information, "Information")
                Call removeButton()
                Call kondisiAwal()
            End If
        End If
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Call kosongkanData()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If price.Text = "" Or serviceName.Text = "" Or estDuration.Text = "" Then
            MsgBox("Mohon isi data dengan lengkap!", MsgBoxStyle.Critical, "Error")
        Else
            If serviceId.Text = "" Then
                query = "insert into Service(name,idcategory,idunit,priceunit,EstimationDuration) values('{0}','{1}','{2}','{3}','{4}')"
                query = String.Format(query, serviceName.Text, idcategory, idunit, price.Text, estDuration.Text)
                aksi(query)
                MsgBox("Insert Data Service Berhasil!", MsgBoxStyle.Information, "Information")
                Call removeButton()
                Call kondisiAwal()
            Else
                query = "update Service set name='" & serviceName.Text & "',idcategory='" & idcategory & "',idunit='" & idunit & "',priceunit='" & price.Text & "',EstimationDuration='" & estDuration.Text & "' where id='" & serviceId.Text & "'"
                aksi(query)
                MsgBox("Update Data Service Berhasil!", MsgBoxStyle.Information, "Information")
                Call removeButton()
                Call kondisiAwal()
            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        MainForm.Show()
        Me.Hide()
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            Call removeButton()
            query = "select a.id 'Service ID',a.name,b.name,c.name,a.priceunit,a.estimationDuration from Service a,Category b,Unit c where a.idcategory=b.id and a.idunit=c.id and a.name like '%" & TextBox1.Text & "%'"
            dgv.AutoGenerateColumns = True
            dgv.DataSource = read(query)
            Call addButton()
        End If
    End Sub
End Class
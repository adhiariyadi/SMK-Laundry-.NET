Imports System.Data.SqlClient
Public Class ManageEmployee
    Dim conn As SqlConnection
    Dim dr As SqlDataReader
    Dim cmd As SqlCommand
    Dim query, idjob As String

    Sub koneksi()
        conn = New SqlConnection("Data Source=DESKTOP-PQDG5SB;Initial Catalog=laundry;Integrated Security=True")
        If conn.State = ConnectionState.Closed Then conn.Open()
    End Sub

    Sub kosongkanData()
        employeeId.Text = ""
        employeeName.Text = ""
        email.Text = ""
        phoneNumber.Text = ""
        address.Text = ""
        dateOfBirth.Text = Date.Now
        salary.Text = ""
        password.Text = ""
        confirmPassword.Text = ""
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
        dgv.Columns.RemoveAt(9)
        dgv.Columns.RemoveAt(8)
    End Sub

    Sub jobTitle()
        query = "select * from job"
        selectJobTitle.DataSource = read(query)
        selectJobTitle.ValueMember = "name"
        selectJobTitle.DisplayMember = "name"
    End Sub

    Sub kondisiAwal()
        query = "select a.id 'Employee Id', a.name, a.email, a.address, a.phonenumber, a.dateofbirth, b.name, a.salary from Employee a, Job b where a.idjob=b.id"
        dgv.DataSource = read(query)
        Call addButton()
        Call kosongkanData()
    End Sub

    Private Sub ManageEmployee_Load_1(sender As Object, e As EventArgs) Handles MyBase.Load
        Call kondisiAwal()
        Call jobTitle()
    End Sub

    Private Sub selectJobTitle_SelectedIndexChanged(sender As Object, e As EventArgs) Handles selectJobTitle.SelectedIndexChanged
        Call koneksi()
        query = "select * from job where name='" & selectJobTitle.Text & "'"
        cmd = New SqlCommand(query, conn)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            idjob = dr.Item("id")
        End If
    End Sub

    Private Sub salary_KeyPress(sender As Object, e As KeyPressEventArgs) Handles salary.KeyPress
        Dim kunci As Short = Asc(e.KeyChar)
        If (e.KeyChar Like "[0-9]" _
            OrElse kunci = Keys.Back) Then
            kunci = 0
        End If
        e.Handled = CBool(kunci)
    End Sub

    Private Sub phoneNumber_KeyPress(sender As Object, e As KeyPressEventArgs) Handles phoneNumber.KeyPress
        Dim kunci As Short = Asc(e.KeyChar)
        If (e.KeyChar Like "[0-9]" _
            OrElse kunci = Keys.Back) Then
            kunci = 0
        End If
        e.Handled = CBool(kunci)
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        MainForm.Show()
        Me.Hide()
    End Sub

    Private Sub dgv_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellContentClick
        If e.ColumnIndex = 8 Then
            employeeId.Text = dgv.CurrentRow.Cells(0).Value
            employeeName.Text = dgv.CurrentRow.Cells(1).Value
            email.Text = dgv.CurrentRow.Cells(2).Value
            phoneNumber.Text = dgv.CurrentRow.Cells(3).Value
            address.Text = dgv.CurrentRow.Cells(4).Value
            dateOfBirth.Text = Date.Parse(dgv.CurrentRow.Cells(5).Value)
            salary.Text = dgv.CurrentRow.Cells(7).Value
        ElseIf e.ColumnIndex = 9 Then
            employeeId.Text = dgv.CurrentRow.Cells(0).Value
            If MessageBox.Show("Yakin mau hapus?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
                query = "delete from Employee where id='" & employeeId.Text & "'"
                aksi(query)
                MsgBox("Delete Data Employee Berhasil!", MsgBoxStyle.Information, "Information")
                Call removeButton()
                Call kondisiAwal()
            Else
                MsgBox("Delete Data Employee Gagal!", MsgBoxStyle.Information, "Information")
                Call removeButton()
                Call kondisiAwal()
            End If
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If email.Text = "" Or employeeName.Text = "" Or phoneNumber.Text = "" Or address.Text = "" Or salary.Text = "" Or password.Text = "" Or confirmPassword.Text = "" Then
            MsgBox("Mohon isi data dengan lengkap!", MsgBoxStyle.Critical, "Error")
        Else
            If password.Text <> confirmPassword.Text Then
                MsgBox("Password tidak sama!", MsgBoxStyle.Critical, "Error")
            Else
                If employeeId.Text = "" Then
                    query = "insert into Employee(Password,Name,Email,Address,PhoneNumber,Dateofbirth,IdJob,Salary) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')"
                    query = String.Format(query, confirmPassword.Text, employeeName.Text, email.Text, address.Text, phoneNumber.Text, dateOfBirth.Value.ToString("yyyy-MM-dd"), idjob, salary.Text)
                    aksi(query)
                    MsgBox("Insert Data Employee Berhasil!", MsgBoxStyle.Information, "Information")
                    Call removeButton()
                    Call kondisiAwal()
                Else
                    query = "update Employee set password='" & confirmPassword.Text & "',name='" & employeeName.Text & "',email='" & email.Text & "',address='" & address.Text & "',phonenumber='" & phoneNumber.Text & "',dateofbirth='" & dateOfBirth.Value.ToString("yyyy-MM-dd") & "',idjob='" & idjob & "',salary='" & salary.Text & "'  where id='" & employeeId.Text & "'"
                    aksi(query)
                    MsgBox("Update Data Employee Berhasil!", MsgBoxStyle.Information, "Information")
                    Call removeButton()
                    Call kondisiAwal()
                End If
            End If
        End If
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Call kosongkanData()
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            Call removeButton()
            query = "select a.id 'Employee Id', a.name, a.email, a.address, a.phonenumber, a.dateofbirth, b.name, a.salary from Employee a, Job b where a.idjob=b.id and a.name like '%" & TextBox1.Text & "%'"
            dgv.AutoGenerateColumns = True
            dgv.DataSource = read(query)
            Call addButton()
        End If
    End Sub
End Class
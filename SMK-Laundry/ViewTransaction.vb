Imports System.Data.SqlClient
Public Class ViewTransaction
    Dim conn As SqlConnection
    Dim dr As SqlDataReader
    Dim cmd As SqlCommand
    Dim dt As DataTable
    Dim query, deposit, header, prepaid As String

    Sub koneksi()
        conn = New SqlConnection("Data Source=DESKTOP-PQDG5SB;Initial Catalog=laundry;Integrated Security=True")
        If conn.State = ConnectionState.Closed Then conn.Open()
    End Sub

    Sub addButton()
        Dim btn As New DataGridViewButtonColumn()
        btn.HeaderText = "Action"
        btn.Text = "Completed"
        btn.Name = "btnCompleted"
        btn.UseColumnTextForButtonValue = True
        dgv.Columns.Add(btn)
    End Sub

    Sub removeButton()
        dgv.Columns.RemoveAt(9)
    End Sub

    Sub kondisiAwal()
        dgv.DataSource = Nothing
        query = "select a.id 'Deposit Id',b.name 'Customer Name',c.name 'Employee Name',e.name 'Service Name',d.idprepaidpackage,d.priceunit,d.totalunit,a.TransactionDatetime,d.CompletedDatetime from HeaderDeposit a,Customer b,Employee c,DetailDeposit d,Service e where a.idcustomer=b.id and a.id=d.iddeposit and a.idemployee=c.id and d.idservice=e.id"
        dgv.DataSource = read(query)
        dgv.AllowUserToAddRows = False
        Call addButton()
        Dim totalPrice, totalUnit As Double
        totalPrice = 0
        totalUnit = 0
        For Each row As DataGridViewRow In dgv.Rows
            totalPrice += row.Cells(5).Value
            totalUnit += row.Cells(6).Value
        Next
        dgv.Rows.Add(Nothing, Nothing, Nothing, Nothing, "Total", totalPrice, totalUnit)
    End Sub

    Private Sub ViewTransaction_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call kondisiAwal()
    End Sub

    Private Sub btnCetak_Click(sender As Object, e As EventArgs) Handles btnCetak.Click
        dt = New DataTable
        With dt
            .Columns.Add("Column1")
            .Columns.Add("Column2")
            .Columns.Add("Column3")
            .Columns.Add("Column4")
            .Columns.Add("Column5")
            .Columns.Add("Column6")
            .Columns.Add("Column7")
            .Columns.Add("Column8")
        End With
        For Each row As DataGridViewRow In dgv.Rows
            dt.Rows.Add(row.Cells(0).Value, row.Cells(1).Value, row.Cells(2).Value, row.Cells(3).Value, row.Cells(4).Value, row.Cells(5).Value, row.Cells(6).Value, row.Cells(7).Value)
        Next
        ReportTransaction.rv.LocalReport.DataSources.Item(0).Value = dt
        ReportTransaction.rv.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout)
        ReportTransaction.Show()
    End Sub

    Private Sub dgv_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellContentClick
        depositId.Text = dgv.CurrentRow.Cells(0).Value
        If e.ColumnIndex = 9 Then
            Call koneksi()
            query = "select * from DetailDeposit where iddeposit='" & depositId.Text & "'"
            cmd = New SqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                deposit = dr.Item("id")
                header = dr.Item("iddeposit")
                prepaid = dr.Item("idprepaidpackage")
            End If
            query = "update DetailDeposit set completeddatetime='" & DateTimePicker1.Value.ToString("yyyy-MM-dd") & "' where id='" & deposit & "'"
            aksi(query)
            query = "update HeaderDeposit set completeestimationdatetime='" & DateTimePicker1.Value.ToString("yyyy-MM-dd") & "' where id='" & header & "'"
            aksi(query)
            query = "update PrepaidPackage set completeddatetime='" & DateTimePicker1.Value.ToString("yyyy-MM-dd") & "' where id='" & prepaid & "'"
            aksi(query)
            MsgBox("Transaction Completed", MsgBoxStyle.Information, "Information")
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        MainForm.Show()
        Me.Hide()
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            Call removeButton()
            query = "select a.id 'Deposit Id',b.name 'Customer Name',c.name 'Employee Name',e.name 'Service Name',d.idprepaidpackage,d.priceunit,d.totalunit,a.TransactionDatetime,d.CompletedDatetime from HeaderDeposit a,Customer b,Employee c,DetailDeposit d,Service e where a.idcustomer=b.id and a.id=d.iddeposit and a.idemployee=c.id and d.idservice=e.id and b.name like '%" & TextBox1.Text & "%'"
            dgv.DataSource = read(query)
            Call addButton()
        End If
    End Sub
End Class
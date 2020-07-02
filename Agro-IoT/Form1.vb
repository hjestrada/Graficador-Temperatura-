Imports System.IO.Ports


Public Class Form1
    Dim serie
    Dim minuto = 0
    Dim lectura
    Dim aux = 0

    Private Sub Chart1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Chart1.Click

    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        GroupBox5.Enabled = False
        GroupBox3.Enabled = False
        GroupBox4.Enabled = False
        Timer1.Enabled = False
        Timer2.Enabled = False


        Try
            For Each sp As String In My.Computer.Ports.SerialPortNames
                ComboBox1.Items.Add(sp)
            Next



        Catch ex As Exception
            MsgBox("Error al listar Puertos Disponibles")
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            serie = My.Computer.Ports.OpenSerialPort(ComboBox1.Text)  'Constructor

  
                Label3.Text = serie.PortName

                'Definir las características de la comunicación
                serie.BaudRate = 9600        'velocidad de comunicaciones
                serie.DataBits = 8            'Longitud para Byte de datos
                serie.Parity = Parity.Even    'paridad(enumeracion parity)
                serie.StopBits = StopBits.Two 'Bits parada después datos
      
            MsgBox("Conectado exitosamente")

            GroupBox5.Enabled = True
            GroupBox3.Enabled = True
            GroupBox4.Enabled = True

        Catch ex As Exception
            MsgBox("Error al intentar conectar")

        End Try
 
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Try
            Timer2.Enabled = False
            serie.Close()           'Cerrar el Puerto Serie
            GroupBox5.Enabled = False
            GroupBox3.Enabled = False
            GroupBox4.Enabled = False
            Timer1.Enabled = False
            Timer2.Enabled = False
        Catch ex As Exception
            MsgBox("Error!")

        End Try

        
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            ListBox1.Enabled = True
            Button3.Enabled = False
            lectura = serie.ReadChar()
            TextBox1.Text = lectura
            Timer1.Enabled = True
            Timer2.Enabled = True
            ListBox1.Items.Add("Minuto:" & aux & " Temp:" & lectura)
            Chart1.Series("Temperatura").Points.AddXY(0, lectura)
        Catch ex As Exception
            MsgBox("Error!")
        End Try
       

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)


    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            Button3.Enabled = True
            ListBox1.Items.Clear()
            Chart1.Series("Temperatura").Points.Clear()


            Timer1.Enabled = False
            Timer2.Enabled = False



            Timer2.Enabled = False
            minuto = minuto + 1

            Label6.Text = "00"
            Label8.Text = "00"
            Label4.Text = "00"
        Catch ex As Exception

        End Try
       

    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Timer2.Interval = 1
        Label4.Text += 1

        If Label4.Text = "60" Then
            Label6.Text += 1
            Label4.Text = 0

        End If
        If Label6.Text = "60" Then
            Label8.Text += 1
            Label6.Text = 0
            aux = Label8.Text
            ListBox1.Items.Add("Minuto:" & aux & " Temp:" & lectura)
            Chart1.Series("Temperatura").Points.AddXY(minuto, lectura)
        End If

        'If Label2.Text = 60 Then
        'Label1.Text += 1
        'Label4.Text = 0
        'End If

    End Sub

    Private Sub Timer1_Tick_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        lectura = serie.ReadChar()
        TextBox1.Text = lectura

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Try
            Button3.Enabled = True
            Timer2.Stop()
        Catch ex As Exception

        End Try

    
    End Sub

    Private Sub ToolStripStatusLabel1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripStatusLabel1.Click
        MsgBox("Dev: Hector J. Estrada @hjestrada Cel: 3133297351")
    End Sub
End Class

using System;
using System.Net.Sockets;
using System.Text;

class ClienteTCP
{
    static void Main(string[] args)
    {
        string nombreCliente = "Ale";

        Console.Write("Ingresa la IP del servidor: ");
        string ip = Console.ReadLine();

        Console.Write("Ingresa el puerto: ");
        int puerto = int.Parse(Console.ReadLine());

        try
        {
            using (TcpClient cliente = new TcpClient())
            {
                cliente.Connect(ip, puerto);
                Console.WriteLine($"Cliente: {nombreCliente} conectado al servidor.");

                NetworkStream stream = cliente.GetStream();

                // 🔥 MENSAJE AUTOMÁTICO AL CONECTARSE
                string mensajeConexion = nombreCliente + " conectado al servidor";
                byte[] datosConexion = Encoding.UTF8.GetBytes(mensajeConexion);
                stream.Write(datosConexion, 0, datosConexion.Length);

                while (true)
                {
                    Console.Write("\nEscribe un comando (SALIR para terminar): ");
                    string mensaje = Console.ReadLine();

                    byte[] datosEnviar = Encoding.UTF8.GetBytes(mensaje);
                    stream.Write(datosEnviar, 0, datosEnviar.Length);

                    if (mensaje.ToUpper() == "SALIR")
                    {
                        Console.WriteLine("Cerrando conexión...");
                        break;
                    }

                    byte[] datosRecibir = new byte[1024];
                    int bytesLeidos = stream.Read(datosRecibir, 0, datosRecibir.Length);

                    string respuesta = Encoding.UTF8.GetString(datosRecibir, 0, bytesLeidos);
                    Console.WriteLine("Respuesta del servidor: " + respuesta);
                }

                stream.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

        Console.WriteLine("Cliente finalizado.");
        Console.ReadKey();
    }
}
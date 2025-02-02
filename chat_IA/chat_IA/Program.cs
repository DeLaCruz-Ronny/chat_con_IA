

using chat_IA;
using OpenAI.Chat;
using System.Text;

var modelo = "gpt-4o";
var cliente = new ChatClient(modelo, Constantes.OpenAIKey);

//Se creara un listado de los mensajes que se envien a la IA para que siga el hilo
//de lo que estamos hablando
var mensajes = new List<ChatMessage>();

Console.WriteLine("IA: Hola soy el asistente del señor Ronny De La Cruz, como puedo ayudarte?");
Console.WriteLine();

while (true)
{

    var sb = new StringBuilder();

    Console.Write("Tu: ");
    var textIn = Console.ReadLine();

    // Si el usuario no escribe nada salimos
    if (string.IsNullOrWhiteSpace(textIn))
    {
        break;
    }

    //Agregamos a la lista los mensajes que el usuario le envia a la IA
    mensajes.Add(new UserChatMessage(textIn));

    Console.WriteLine();
    Console.Write($"IA: ");

    var stream = cliente.CompleteChatStreamingAsync(mensajes);

    // Hacemos este foreach para que muestre el texto letra por letra como lo hacen las IA habitualemente
    await foreach (var actualizacion in stream)
    {
        foreach (var contenido in actualizacion.ContentUpdate)
        {
            sb.Append(contenido.Text);
            Console.Write(contenido.Text);
        }
    }

    mensajes.Add(new AssistantChatMessage(sb.ToString()));


    //var IAresponse = await cliente.CompleteChatAsync(mensajes);
    //var respuestIA = IAresponse.Value.Content[0].Text;
    //mensajes.Add(new AssistantChatMessage(respuestIA));
    //Console.WriteLine($"IA: {respuestIA}");

    Console.WriteLine();
    Console.WriteLine();
}






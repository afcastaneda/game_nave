using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ServicioWeb : MonoBehaviour
{
    public RespuestaRegistro respuestaRegistro;
    // Start is called before the first frame update
    void Start()
    {
        Usuario usuario = new Usuario();
        usuario.document = "1053823340";
        usuario.name = "Andrés Castañeda";
        usuario.email = "afcas@gmail.com";
        StartCoroutine(RegistrarUsuario(usuario));
    }

    public IEnumerator RegistrarUsuario(Usuario datosRegistro)
    {
        var registroJSON = JsonUtility.ToJson(datosRegistro);

        var solicitud = new UnityWebRequest();
        solicitud.url = "http://localhost:3000/api/jugador/register";

        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(registroJSON);
        solicitud.uploadHandler = new UploadHandler(bodyRaw);
        solicitud.downloadHandler = new DownloadHandlerBuffer();
        solicitud.method = UnityWebRequest.kHttpVerbPOST;
        solicitud.SetRequestHeader("Content-Type", "application/json");

        solicitud.timeout = 10;

        yield return solicitud.SendWebRequest();

        if (solicitud.result == UnityWebRequest.Result.ConnectionError)
        {
            respuestaRegistro.mensaje = "Conexión Fallida";
        }
        else
        {
            respuestaRegistro = (RespuestaRegistro)JsonUtility.FromJson(solicitud.downloadHandler.text, typeof(RespuestaRegistro));
        }
        solicitud.Dispose();
    }

   
}

using System.IO;
using UnityEngine;

public class Language_Manager : MonoBehaviour
{
    private string txtFile; // referencia al archivo .txt

    public int Language_ID = 0; //0 is spanish, 1 is english

    static public string curLanguage;

    void Awake()
    {

        switch (Language_ID) {

            default:
                txtFile = "Assets/Scripts/Languages/Game/es.txt"; break;
            case 1:
                txtFile = "Assets/Scripts/Languages/Game/en.txt"; break;
        }

        string fileContents = File.ReadAllText(txtFile); // lee el archivo .txt y lo asigna a la variable fileContents
        curLanguage = fileContents;
        
    }

    public string GetWord(string identifier)
    {

        int pos = curLanguage.IndexOf(identifier);

        string phrase = "";

        for (int i = pos + identifier.Length - 1 ; i <= 1000; i++) {

            if (curLanguage[i] == '\"' && curLanguage[i + 1] == ',')
            {

                break;

            }
            else {

                if (curLanguage[i] != ':' && curLanguage[i + 1] !=  ' ')
                {
                    phrase += curLanguage[i];

                }
            } 

        }

        return phrase;


    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ApiTestHelper : MonoBehaviour
{
    [Header("Api call Setup")]
    public string url = "";
    public Dictionary<string, string> parameters = new();

    [Header("Objects")]
    public TextMeshProUGUI resultField;
    public RawImage ImageField;


    public void MakeApiCall()
    {
        IEnumerator apiCall = ApiHelper.Get(url, parameters, OnSuccess, OnFailure);

        resultField.text = "In Progress";

        StartCoroutine(apiCall);
    }

    private void OnFailure(Exception exception)
    {
        resultField.text = "Call error:" + "<br>" + exception.Message;

    }

    private void OnSuccess(string result)
    {
        resultField.text = result;
    }

    //Pokemon parse
    [Serializable]
    public class Pokemon
    {
        public string name;
        public List<SlotType> types;
        public Sprites sprites;
    
        [Serializable]
        public class SlotType
        {
            public int slot;
            public Type type;

            [Serializable]
            public class Type
            {
                public string name;
            }
        }

        [Serializable]
        public class Sprites
        {
            public string front_default;
        }
    }
    

    public void MakePokemonApiCall()
    {

        IEnumerator apiCall = ApiHelper.Get<Pokemon>(url, parameters, OnPokemonSuccess, OnPokemonFailure);

        resultField.text = "In Progress";

        StartCoroutine(apiCall);
    }

    private void OnPokemonFailure(Exception exception)
    {
        resultField.text = "Call error:" + "<br>" + exception.Message;

    }

    private void OnPokemonSuccess(Pokemon result)
    {
        resultField.text = "Name: " + result.name;
        resultField.text += "<br>Types: ";

        foreach (Pokemon.SlotType slotType in result.types)
        {
            resultField.text += " " + slotType.type.name;
        }
        IEnumerator imageApiCall = ApiHelper.GetTexture(result.sprites.front_default, OnImageSuccess, OnImageFailure);

        StartCoroutine(imageApiCall);
    }

    private void OnImageFailure(Exception exception)
    {
        ImageField.texture = null;
        ImageField.color = Color.red;
    }
    private void OnImageSuccess(Texture texture)
    {
        ImageField.color = Color.white;
        ImageField.texture = texture;
    }
}

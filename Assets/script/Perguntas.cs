using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.UI;
using System.Security.Cryptography;

public class Perguntas : MonoBehaviour
{
    //Pega API
    private string apiUrl = "https://opentdb.com/api.php?amount=15&category=15&difficulty=hard&type=boolean"; //Exemplo de URL para obter as perguntas

    //Cria Lista
    [System.Serializable]
    public class Question
    {
        public string question;
        public string[] options;
        public int correctOptionIndex;

    }

    //Controla a pontuação
    private int point = 0;
    public Text pointText; //Referência ao elemento Text onde você exebirá a pontuação
    public GameObject ObjQuestion;

    public List<Question> triviaQuestions = new List<Question>();
    public Text questionText; //Referência ao elemento Text onde você exibirá a pergunta
    public Text[] optionTexts; //Referência aos elementos Text onde você exibirá as opções

    private int currentQuestionIndex = 0; //indice atual da questão
 
    void Start()
    {
        ObjQuestion.SetActive(false);
        StartCoroutine(GetTriviaQuestions()); //Chama a rotina para start das perguntas
        pointText.text = point.ToString(); //inicializa a pontuação em zero elemento text
        
    }

    IEnumerator GetTriviaQuestions()
    {
        //Se comunica com a API através da URL
        using (UnityWebRequest webRequest = UnityWebRequest.Get(apiUrl))
        {


            yield return webRequest.SendWebRequest(); //faz a requisição para a API

            //Exibe erro, em caso de erro
            if (webRequest.result == UnityWebRequest.Result.ConnectionError)
            {

                Debug.LogError("Erro de conexão:" + webRequest.error);

            }

            //Realiza o tratamento de perguntas
            else if (webRequest.result == UnityWebRequest.Result.Success)
            {
                string jsonText = webRequest.downloadHandler.text;

                //parse JSON
                JSONNode JsonData = JSON.Parse(jsonText);
                JSONArray questionsArray = JsonData["results"].AsArray;

                //Limpar a lista de perguntas
                triviaQuestions.Clear();

                foreach (JSONNode questionData in questionsArray)
                {

                    Question newQuestion = new Question();
                    newQuestion.question = questionData["question"];
                    newQuestion.correctOptionIndex = Random.Range(0, 2); //escolha uma opção coreta aleatória
                    Debug.Log(newQuestion.question);
                    newQuestion.options = new string[2];
                    newQuestion.options[newQuestion.correctOptionIndex] = questionData["correct_answer"];

                    for (int i = 0, j = 0; i < 2; i++)
                    {
                        if (i != newQuestion.correctOptionIndex)
                        {

                            newQuestion.options[1] = questionData["incorrect_answers"][j];
                            j++;
                        }

                    }

                    triviaQuestions.Add(newQuestion);
                }

                //Agora, você
                NextQuestion();

            }


        }
    }

    void DisplayQuestion(Question question)
    {

        questionText.text = question.question;

        for (int i = 0; i < question.options.Length; i++)
        {

            optionTexts[i].text = question.options[i];
        }

    }

    public void NextQuestion()
    {

        if (currentQuestionIndex < triviaQuestions.Count)
        {

            DisplayQuestion(triviaQuestions[currentQuestionIndex]);
        }
  

        currentQuestionIndex++;
    }
    
   public void CheckAnswer(int selectedOptionIndex)
    {

        if (currentQuestionIndex < triviaQuestions.Count)
        {

            if (selectedOptionIndex == triviaQuestions[currentQuestionIndex].correctOptionIndex)
            {

                point++;
                pointText.text = point.ToString();
            }
            if (currentQuestionIndex < triviaQuestions.Count)
            {

                NextQuestion();
            }
        }
        else
        {

            questionText.text = "FIM DE PERGUNTAS";
            ObjQuestion.SetActive(false);
        }
    }
 


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

public class AccountCanvas : MonoBehaviour
{
    [SerializeField]
    private Button btnSubmit;

    [SerializeField]
    private Button btnCancel;

    [SerializeField]
    private InputField emailInput;

    [SerializeField]
    private InputField passwordInput;

    [SerializeField]
    private Text txtEmail;

    [SerializeField]
    private Text txtPassword;

    [SerializeField]
    private GameObject responsePopUp;

    [SerializeField]
    private Button btnClosePopUp;

    [SerializeField]
    private Button btnEmailLogin;

    [SerializeField]
    private Text txtResponseInfo;

    private (string email, string password) inputValue; //Emailとパスワード登録用



    // Start is called before the first frame update
    void Start()
    {
        responsePopUp.SetActive(false);

        //ボタンの登録
        btnSubmit?.OnClickAsObservable()
            .ThrottleFirst(TimeSpan.FromSeconds(0.5f))
            .Subscribe(_ => OnClickSubmit());

        btnCancel?.OnClickAsObservable()
            .ThrottleFirst(TimeSpan.FromSeconds(0.5f))
            .Subscribe(_ => OnClickCancel());

        btnClosePopUp?.OnClickAsObservable()
            .ThrottleFirst(TimeSpan.FromSeconds(0.5f))
            .Subscribe(_ => OnClickCloseCompletePopUp());

        btnEmailLogin?.OnClickAsObservable()
            .ThrottleFirst(TimeSpan.FromSeconds(0.5f))
            .Subscribe(_ => OnClickEmailLogin());

        //InputField
        emailInput?.OnEndEditAsObservable()
            .Subscribe(x => UpdateDisplayEmail(x));

        passwordInput?.OnEndEditAsObservable()
            .Subscribe(x => UpdateDisplayPassword(x));

    }


    /// <summary>
    /// Emailの値と表示の更新
    /// </summary>
    /// <param name="newEmail"></param>
    private void UpdateDisplayEmail(string newEmail)
    {
        txtEmail.text = newEmail;
        inputValue.email = newEmail;
        Debug.Log(inputValue);
    }


    /// <summary>
    /// パスワードの値と表示の更新
    /// </summary>
    /// <param name="newPassword"></param>
    private void UpdateDisplayPassword(string newPassword)
    {
        txtPassword.text = newPassword;
        inputValue.password = newPassword;
        Debug.Log(inputValue);
    }


    /// <summary>
    /// OKボタンを押した時の処理
    /// </summary>
    private async void OnClickSubmit()
    {
        Debug.Log("OK アカウント連携の承認開始");

        //Emailとパスワードを利用して、ユーザーアカウントの連携を試みる
        bool isLink = await PlayFabAccountLink.SetEmailAndPasswordAsync(inputValue.email, inputValue.password);

        if (isLink)
        {
            Debug.Log("連携完了");

            txtResponseInfo.text = "アカウント連携が完了しました。";
            responsePopUp.SetActive(true);
        }
        else
        {
            Debug.Log("アカウント連携が失敗しました。");

            txtResponseInfo.text = "アカウント連携が失敗しました。";
            responsePopUp.SetActive(true);
        }

    }


    /// <summary>
    /// NGボタンを押した時の処理
    /// </summary>
    private void OnClickCancel()
    {
        this.gameObject.SetActive(false);

        Debug.Log("NG");
    }


    /// <summary>
    /// CompletePopUpをタップした時の処理
    /// </summary>
    private void OnClickCloseCompletePopUp()
    {
        responsePopUp.SetActive(false);

        this.gameObject.SetActive(false);
    }


    /// <summary>
    /// Emailでログインボタンを押した時の処理
    /// </summary>
    private async void OnClickEmailLogin()
    {
        //Emailでログインを試みる isLogin=trueならログイン成功
        // TODO 次の手順でLoginManagerに該当するメソッドを作成する
        //(bool isLogin,string log)response=await LoginManager.LoginEmailAndPasswordAsync(inputValue.email,inputValue.password);

        // TODO isLogin分岐

        //Debug.Log(response.log);

        //txtResponseInfo.text = response.log;
        responsePopUp.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

using Core;
using TMPro;

namespace WorldTicket
{
    public class FileSelectUI : MonoBehaviour
    {
        [SerializeField]
        Button OkButton;

        [SerializeField]
        TMP_InputField FileInput;

        //[SerializeField]
        //TextMeshProUGUI Dialog;

        [SerializeField]
        Text Dialog;

        private void Start()
        {
            if (ManagerCore.Data.CheckConfigs() == false)
            {
                Dialog.text = "설정 파일이 새로 생성되었습니다. \n 파일 확인 후, 다시 실행해 주세요.";
                OkButton.interactable = false;
                return;
            }

            OkButton.onClick.AddListener(OnOkButtonClicked);
        }

        void OnOkButtonClicked()
        {
            string path = FileInput.text;

            if (string.IsNullOrWhiteSpace(path) == true)
            {
                Dialog.DOText("전체 파일 경로를 입력해주세요. (확장자 포함)", 0.5f).SetEase(Ease.Linear);
                //Dialog.text = "전체 파일 경로를 입력해주세요. (확장자 포함)";
                return;
            }

            if (ManagerCore.Data.LoadConfigs() == false)
            {
                Dialog.DOText("설정 파일을 읽는 중 문제가 발생했습니다.", 0.5f).SetEase(Ease.Linear);
                //Dialog.text = "설정 파일을 읽는 중 문제가 발생했습니다.";
                return;
            }

            string err = "";
            if (ManagerCore.Data.Load(path, ref err) == false)
            {
                Dialog.DOText(err, 0.5f).SetEase(Ease.Linear);
                //Dialog.text = err;
                return;
            }

            ManagerCore.Scene.CurrentScene<MainScene>().Load();

            Dialog.DOText("정상적으로 로드 완료했습니다.", 0.5f).SetEase(Ease.Linear);
            //Dialog.text = "정상적으로 로드 완료했습니다.";

            Destroy(gameObject, 2.0f);
        }
    }
}

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
                Dialog.text = "���� ������ ���� �����Ǿ����ϴ�. \n ���� Ȯ�� ��, �ٽ� ������ �ּ���.";
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
                Dialog.DOText("��ü ���� ��θ� �Է����ּ���. (Ȯ���� ����)", 0.5f).SetEase(Ease.Linear);
                //Dialog.text = "��ü ���� ��θ� �Է����ּ���. (Ȯ���� ����)";
                return;
            }

            if (ManagerCore.Data.LoadConfigs() == false)
            {
                Dialog.DOText("���� ������ �д� �� ������ �߻��߽��ϴ�.", 0.5f).SetEase(Ease.Linear);
                //Dialog.text = "���� ������ �д� �� ������ �߻��߽��ϴ�.";
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

            Dialog.DOText("���������� �ε� �Ϸ��߽��ϴ�.", 0.5f).SetEase(Ease.Linear);
            //Dialog.text = "���������� �ε� �Ϸ��߽��ϴ�.";

            Destroy(gameObject, 2.0f);
        }
    }
}

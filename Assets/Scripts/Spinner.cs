using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Core;
using DG.Tweening;

namespace WorldTicket
{
    [System.Serializable]
    class SpinnerUIContext : UIContext
    {
        public Property<string> Number1 = new();
        public Property<string> Number2 = new();
        public Property<string> Number3 = new();
    }

    public class Spinner : BaseUI, ICustomProperty<int>
    {
        [SerializeField]
        SpinnerUIContext Context = new();

        const float ShowInterval1 = 0.2f;
        const float ShowInterval2 = 0.08f;
        const float ShowInterval3 = 0.12f;
        const float StopDuration1 = 1.5f;
        const float StopDuration2 = 5.5f;
        const float StopDuration3 = 3.0f;

        MainUI mainUI;

        int targetNumber;
        int teamIndex;

        readonly int[] target = new int[] { 0, 0, 0 };
        readonly int[] currentNumber = new int[]{ 0, 0, 0 };
        readonly float[] stopDurations = new float[] { StopDuration1, StopDuration2, StopDuration3 };
        readonly float[] showIntervals = new float[] { ShowInterval1, ShowInterval2, ShowInterval3 };
        readonly bool[] isSpinning = new bool[] { false, false, false };
        readonly bool[] isSpinningFinished = new bool[] { false, false, false };

        AudioClip spinningClip;
        AudioClip resultClip;

        private void Start()
        {
            mainUI = ManagerCore.UI.SceneUI<MainUI>();

            spinningClip = ManagerCore.Resource.Load<AudioClip>("Sounds/spinning");
            resultClip = ManagerCore.Resource.Load<AudioClip>("Sounds/result");

            //if (spinningClip == null) Debug.LogError("Can not find spinning.mp3");
            //if (resultClip == null) Debug.LogError("Can not find result.mp3");
        }

        public int GetData()
        {
            return targetNumber;
        }

        public void SetData(int value)
        {
            targetNumber = value;
            SetTargetNumber(value);
        }


        public void StartSpin(int targetNumber, int teamIndex)
        {
            SetData(targetNumber);
            this.teamIndex = teamIndex;
            mainUI.ActiveAllButtons(false);

            //Debug.Log($"Set: {target[0]} {target[1]} {target[2]}");

            SetNumber(0, 0);
            SetNumber(0, 0);
            SetNumber(2, 0);
            
            StartCoroutine(Spin(0));
            StartCoroutine(Spin(1));
            StartCoroutine(Spin(2));
        }

        public void StopSpin()
        {
            ManagerCore.Sound.PlaySound(spinningClip, Core.AudioType.Effect);
            for (int i = 0; i < 3; ++i) isSpinning[i] = false;
        }

        void SetTargetNumber(int targetNumber)
        {
            target[0] = targetNumber / 100;
            target[1] = targetNumber / 10 % 10;
            target[2] = targetNumber % 10;
        }

        IEnumerator Spin(int i)
        {
            if (isSpinning[i] == false)
            {
                isSpinningFinished[i] = false;
                isSpinning[i] = true;

                while (isSpinning[i])
                {
                    currentNumber[i] = (currentNumber[i] + 1) % 10;
                    SetNumber(i, currentNumber[i]);

                    yield return new WaitForSeconds(showIntervals[i]);
                }

                int t = currentNumber[i];
                DOTween.To(() => t, x => t = x, target[i] + 10, stopDurations[i])
                    .SetEase(Ease.Linear)
                    .OnUpdate(() => SetNumber(i, t % 10))
                    .OnComplete(() => { isSpinningFinished[i] = true; CheckAllStopped(); });
            }
        }

        void CheckAllStopped()
        {
            for(int i=0; i<3; ++i)
            {
                if (isSpinningFinished[i] == false) return;
            }

            mainUI.ActiveAllButtons(true);
            if (mainUI.SelectedCardPanel() != null)
            {
                mainUI.SelectedCardPanel().ActivePushButton(true);
                mainUI.SelectedCardPanel().AddTeam(ManagerCore.Data.Teams[teamIndex]);
            }

            ManagerCore.Sound.PlaySound(resultClip, Core.AudioType.Effect);
        }

        public void SetNumber(int i, int n)
        {
            if (i == 0) Context.Number1.Value = n.ToString();
            else if (i == 1) Context.Number2.Value = n.ToString();
            else if (i == 2) Context.Number3.Value = n.ToString();
        }

        #region BaseUI Editor 
#if UNITY_EDITOR
        protected override List<string> GetContextFieldsNames()
        {
            List<string> list = new();
            foreach (var name in Context.GetType().GetFields())
            {
                list.Add(name.Name);
            }
            return list;
        }

        protected override object GetContextFieldValue(string fieldName)
        {
            return Context.GetType().GetField(fieldName).GetValue(Context);
        }
#endif
        #endregion
    }
}

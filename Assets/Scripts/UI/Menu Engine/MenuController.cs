using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MenuEngine
{
    [RequireComponent(typeof(AudioSource))]
    public class MenuController : MonoBehaviour
    {
        public delegate void OnPageChangedListener(string newPage);

        public event OnPageChangedListener OnPageChanged;

        [SerializeField]
        private Page[] pages;

        [SerializeField]
        private string StartPageName;

        [SerializeField]
        private UnityEngine.EventSystems.EventSystem eventSystem;

        private Page currentPage;


        public string StartPage { get { return StartPageName; } }

        public string CurrentPage
        {
            get
            {
                if (currentPage != null)
                    return currentPage.Name;
                else
                    return StartPageName;
            }
        }

        private void OnEnable()
        {
            currentPage = pages[0];
            for (int i = 0; i < pages.Length;i++)
            {
                if (CompareStrings(pages[i].Name, StartPageName))
                {
                    currentPage = pages[i];
                }
                else
                {
                    pages[i].PageObject.SetActive(false);
                }
            }
            currentPage.PageObject.SetActive(true);
        }

        public void SetPage(string pageName)
        {
            for (int i = 0; i < pages.Length;i++)
            {
                if (CompareStrings(pages[i].Name, pageName))
                {
                    bool hastransition = false;
                    if (currentPage.transitions != null && currentPage.transitions.Length > 0)
                    {
                        for (int t = 0;t < currentPage.transitions.Length;t++)
                        {
                            Transition transition = currentPage.transitions[t];
                            if (CompareStrings(transition.transition, pageName))
                            {  
                                if (transition.TransitionType == Transition.TransitionTypeEnum.None)
                                {
                                    if (transition.hideCurrent)
                                        currentPage.PageObject.SetActive(false);
                                }
                                else
                                {
                                    currentPage.transitions[t].transitionAnimation.Animate(currentPage, pages[i], currentPage.transitions[t]);
                                    hastransition = true;
                                }
                                break;
                            }
                        }
                    }
                    currentPage = pages[i];
                    OnPageChanged?.Invoke(currentPage.Name);
                    if (!hastransition)
                        currentPage.PageObject.SetActive(true);

                    eventSystem.SetSelectedGameObject(currentPage.FirstSelected.gameObject);

                    break;
                }
            }
        }

        private bool CompareStrings(string string1, string string2)
        {
            return string1.ToLowerInvariant().Replace(" ", "").Equals(string2.ToLowerInvariant().Replace(" ", ""),
                System.StringComparison.CurrentCultureIgnoreCase);
        }

        public void LoadScene(string scene)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
        }

        public void Quit()
        {
            Application.Quit();
        }

    }
}

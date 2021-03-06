﻿using System.Collections;
using UnityEngine;

namespace FCSAlterraIndustrialSolutions.Models.Abstract
{
    public abstract class AIDisplay : MonoBehaviour
    {
        /// <summary>
        /// The max distance to the screen
        /// </summary>
        public float MAX_INTERACTION_DISTANCE { get; set; } = 2.5f;

        public int CurrentPage = 1;

        public int MaxPage = 1;

        public int ITEMS_PER_PAGE;

        /// <summary>
        /// Changes page by a certain amount
        /// </summary>
        /// <param name="amountToChangePageBy"></param>
        public abstract void ChangePageBy(int amountToChangePageBy);

        public abstract void OnButtonClick(string btnName, object tag);

        public abstract void ItemModified<T>(T item);

        public abstract bool FindAllComponents();

        public virtual void ShutDownDisplay()
        {
            StartCoroutine(ShutDown());
        }

        public virtual void PowerOnDisplay()
        {
            StartCoroutine(PowerOn());
        }

        public virtual void PowerOffDisplay()
        {
            StartCoroutine(PowerOff());
        }

        public abstract IEnumerator PowerOff();

        public abstract IEnumerator PowerOn();

        public abstract IEnumerator ShutDown();

        public abstract IEnumerator CompleteSetup();

    }
}

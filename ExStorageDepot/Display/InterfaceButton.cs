﻿using FCSCommon.Enums;
using FCSCommon.Utilities;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ExStorageDepot.Display
{
    /// <summary>
    /// This class is a component for all interface buttons except the color picker and the paginator.
    /// </summary>
    internal class InterfaceButton : OnScreenButton, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
    {
        #region Public Properties

        /// <summary>
        /// The pages to change to.
        /// </summary>
        public GameObject ChangePage { get; set; }
        public string BtnName { get; set; }
        public Color HOVER_COLOR { get; set; } = new Color(0.07f, 0.38f, 0.7f, 1f);
        public Color STARTING_COLOR { get; set; } = Color.white;
        public Text TextComponent { get; set; }
        public int SmallFont { get; set; } = 140;
        public int LargeFont { get; set; } = 180;
        public object Tag { get; set; }
        public float IncreaseButtonBy { get; set; }
        public string HoverItemName { get; set; } = "Hover";

        public Action<string, object> OnButtonClick;
        private bool _inFocus;

        #endregion

        #region Public Methods

        public void OnEnable()
        {
            if (string.IsNullOrEmpty(BtnName)) return;

            QuickLogger.Debug($"Button Name:{BtnName} || Button Mode {ButtonMode}", true);

            switch (this.ButtonMode)
            {
                case InterfaceButtonMode.TextScale:
                    this.TextComponent.fontSize = this.TextComponent.fontSize;
                    break;
                case InterfaceButtonMode.TextColor:
                    this.TextComponent.color = this.STARTING_COLOR;
                    break;
                case InterfaceButtonMode.Background:
                    if (GetComponent<Image>() != null)
                    {
                        GetComponent<Image>().color = this.STARTING_COLOR;
                    }
                    break;
                case InterfaceButtonMode.BackgroundScale:
                    if (this.gameObject != null)
                    {
                        this.gameObject.transform.localScale = this.gameObject.transform.localScale;
                    }
                    break;
                case InterfaceButtonMode.HoverImage:
                    transform.gameObject.FindChild(HoverItemName).SetActive(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        #endregion

        #region Public Overrides

        public override void OnDisable()
        {
            base.OnDisable();

            if (string.IsNullOrEmpty(BtnName)) return;

            QuickLogger.Debug($"Button Name:{BtnName} || Button Mode {ButtonMode}", true);

            switch (this.ButtonMode)
            {
                case InterfaceButtonMode.TextScale:
                    this.TextComponent.fontSize = this.TextComponent.fontSize;
                    break;
                case InterfaceButtonMode.TextColor:
                    this.TextComponent.color = this.STARTING_COLOR;
                    break;
                case InterfaceButtonMode.Background:
                    if (GetComponent<Image>() != null)
                    {
                        GetComponent<Image>().color = this.STARTING_COLOR;
                    }
                    break;
                case InterfaceButtonMode.BackgroundScale:
                    if (this.gameObject != null)
                    {
                        this.gameObject.transform.localScale = this.gameObject.transform.localScale;
                    }
                    break;
            }
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);

            if (_inFocus) return;

            if (this.IsHovered)
            {
                switch (this.ButtonMode)
                {
                    case InterfaceButtonMode.TextScale:
                        this.TextComponent.fontSize = this.LargeFont;
                        break;
                    case InterfaceButtonMode.TextColor:
                        this.TextComponent.color = this.HOVER_COLOR;
                        break;
                    case InterfaceButtonMode.Background:
                        if (GetComponent<Image>() != null)
                        {
                            GetComponent<Image>().color = this.HOVER_COLOR;
                        }
                        break;
                    case InterfaceButtonMode.BackgroundScale:
                        if (this.gameObject != null)
                        {
                            this.gameObject.transform.localScale +=
                                new Vector3(this.IncreaseButtonBy, this.IncreaseButtonBy, this.IncreaseButtonBy);
                        }
                        break;
                    case InterfaceButtonMode.HoverImage:
                        transform.gameObject.FindChild(HoverItemName).SetActive(true);
                        break;
                }
            }
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);

            if (_inFocus) return;

            switch (this.ButtonMode)
            {
                case InterfaceButtonMode.TextScale:
                    this.TextComponent.fontSize = this.SmallFont;
                    break;
                case InterfaceButtonMode.TextColor:
                    this.TextComponent.color = this.STARTING_COLOR;
                    break;
                case InterfaceButtonMode.Background:
                    if (GetComponent<Image>() != null)
                    {
                        GetComponent<Image>().color = this.STARTING_COLOR;
                    }
                    break;
                case InterfaceButtonMode.BackgroundScale:
                    if (this.gameObject != null)
                    {
                        this.gameObject.transform.localScale -=
                            new Vector3(this.IncreaseButtonBy, this.IncreaseButtonBy, this.IncreaseButtonBy);
                    }
                    break;
                case InterfaceButtonMode.HoverImage:
                    transform.gameObject.FindChild(HoverItemName).SetActive(false);
                    break;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);

            if (this.IsHovered)
            {
                //if (ButtonType == FCSDeepDrillerButtonType.ListItem)
                //{
                //    Focus();
                //}

                QuickLogger.Debug($"Clicked Button: {this.BtnName}", true);
                OnButtonClick?.Invoke(this.BtnName, this.Tag);
            }
        }
        #endregion

        public void RemoveFocus()
        {
            _inFocus = false;
            transform.gameObject.FindChild(HoverItemName).SetActive(false);
        }

        internal void Focus()
        {
            _inFocus = true;
            transform.gameObject.FindChild(HoverItemName).SetActive(true);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointCounter : MonoBehaviour
{
   [SerializeField] private Text counterText;
   public int Points { get; private set; }

   private event Action CounterChanged;
   
   private void Start()
   {
      gameObject.SetActive(true);
   }

   private void OnEnable()
   {
      CounterChanged += OnCounterChanged;
   }

   private void OnDisable()
   {
      CounterChanged -= OnCounterChanged;
   }

   private void OnCounterChanged()
   {
      counterText.text = "Points: " + Points;
   }

   public void ChangeValue(int value)
   {
      Points += value;
      CounterChanged?.Invoke();
   }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryButton : MonoBehaviour
{
    public int categoryID;
    private Button categoryButton;

    private void Awake()
    {
        categoryButton = GetComponent<Button>();
    }

    void Start()
    {
        categoryButton.onClick.AddListener(OnCategoryButton);
    }

    void Update()
    {
        
    }

    private void OnCategoryButton()
    {

    }
}

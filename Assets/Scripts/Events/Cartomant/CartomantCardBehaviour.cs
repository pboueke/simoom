﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartomantCardBehaviour : MonoBehaviour {

    public string playerEventColliderName = "PlayerEventCollider";

    private bool inTrigger = false;

    private Renderer rend;
    private CartomantDeckBehaviour deck;

    void Start () {
        rend = GetComponent<Renderer>();
        deck = transform.parent.gameObject.GetComponent<CartomantDeckBehaviour>();

        hideHighLight();
    }
	
	void Update () {
        if (inTrigger && Input.GetKeyDown(KeyCode.E))
        {
            // activate the event
            deck.applyEffect();
        }
    }

    void OnTriggerEnter(Collider col)
    {
        inTrigger = true;

        if (deck.yetToBeUsed && col.gameObject.name == playerEventColliderName)
        {
            showHighlight();
        }
    }

    void OnTriggerExit(Collider col)
    {
        inTrigger = false;

        if (deck.yetToBeUsed && col.gameObject.name == playerEventColliderName)
        {
            hideHighLight();
        }
    }

    // highlight funcions
    private void showHighlight()
    {
        // shader defined at /assets/shaders/$self_illuminated_outlined_diffuse_113.shader
        //rend.material.shader = Shader.Find("Self-Illumin/Outlined Diffuse");
        rend.material.color = Color.HSVToRGB(0.5f,1.0f,0.8f);

    }

    private void hideHighLight()
    {
        //rend.material.shader = Shader.Find("Diffuse");
        rend.material.color = Color.HSVToRGB(0.0f, 1.0f, 0.8f);
    }
}

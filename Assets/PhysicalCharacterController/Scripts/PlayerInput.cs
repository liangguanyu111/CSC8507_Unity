using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
	public float speed = 5;
	public float jumpHeight = 15;
	public PhysicalCC physicalCC;

	public Transform bodyRender;
	IEnumerator sitCort;
	public bool isSitting;

	public bool enhanceJump = false;

	public ParticleSystem gunFire;
	public ParticleSystem paintBullet;
	public ParticleSystem SplashParticle;
	public ParticleSystem SubEmitter;
	public ParticlesController particleController;

	private Animator an;

    private void Start()
    {
		an = this.GetComponentInChildren<Animator>();
    }
    void Update()
	{
		if (physicalCC.isGround)
		{
			Vector2 input = new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));

			physicalCC.moveInput = Vector3.ClampMagnitude(transform.forward
							* input.x
							+ transform.right
							* input.y, 1f) * speed;

			if(input!=Vector2.zero)
            {
				an.SetBool("Move", true);
            }else
            {
				an.SetBool("Move", false);
			}


			if (Input.GetKeyDown(KeyCode.Space))
			{
				physicalCC.inertiaVelocity.y = 0f;
				if(enhanceJump)
                {
					physicalCC.inertiaVelocity.y += 20;
					enhanceJump = false;
				}
				physicalCC.inertiaVelocity.y += jumpHeight;
			}

			if (Input.GetKeyDown(KeyCode.C) && sitCort == null)
			{
				sitCort = sitDown();
				StartCoroutine(sitCort);
			}

			if(Input.GetMouseButtonDown(0))
            {
				
				gunFire.gameObject.SetActive(true);
				gunFire.Play();
            }
			else if(Input.GetMouseButtonUp(0))
            {
				gunFire.gameObject.SetActive(false);
				gunFire.Pause();
			}
		}
	}


	public void ColorSwap(Color newColor)
    {
		Material[] material = gunFire.GetComponent<Renderer>().materials;
		material[0].SetColor("_BaseColor", new Color(newColor.r,newColor.g,newColor.b));
		material[1].SetColor("_BaseColor", new Color(newColor.r, newColor.g, newColor.b));

		Material[] material2 = paintBullet.GetComponent<Renderer>().materials;
		material2[0].SetColor("_BaseColor", new Color(newColor.r, newColor.g, newColor.b));
		material2[1].SetColor("_BaseColor", new Color(newColor.r, newColor.g, newColor.b));


		Material material3 = SubEmitter.GetComponent<Renderer>().material;
		material3.SetColor("_BaseColor", new Color(newColor.r, newColor.g, newColor.b));


		Material[] materials4 = SplashParticle.GetComponent<Renderer>().materials;
		materials4[0].SetColor("_BaseColor", new Color(newColor.r, newColor.g, newColor.b));
		materials4[1].SetColor("_BaseColor", new Color(newColor.r, newColor.g, newColor.b));

		particleController.paintColor = new Color(newColor.r, newColor.g, newColor.b);
	}

	IEnumerator sitDown()
	{
		if (isSitting && Physics.Raycast(transform.position, Vector3.up, physicalCC.cc.height * 1.5f))
		{
			sitCort = null;
			yield break;
		}
		isSitting = !isSitting;

		float t = 0;
		float startSize = physicalCC.cc.height;
		float finalSize = isSitting ? physicalCC.cc.height / 2 : physicalCC.cc.height * 2;

		Vector3 startBodySize = bodyRender.localScale;
		Vector3 finalBodySize = isSitting ? bodyRender.localScale - Vector3.up * bodyRender.localScale.y / 2f : bodyRender.localScale + Vector3.up * bodyRender.localScale.y;

		

		speed = isSitting ? speed / 2 : speed * 2;
		jumpHeight = isSitting ? jumpHeight * 3 : jumpHeight / 3;
		
		while (t < 0.2f)
		{
			t += Time.deltaTime;
			physicalCC.cc.height = Mathf.Lerp(startSize, finalSize, t / 0.2f);
			bodyRender.localScale = Vector3.Lerp(startBodySize, finalBodySize, t / 0.2f);
			yield return null;
		}

		sitCort = null;
		yield break;
	}
}

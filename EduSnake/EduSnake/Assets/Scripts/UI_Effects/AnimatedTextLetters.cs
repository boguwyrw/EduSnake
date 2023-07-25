using System.Collections;
using UnityEngine;
using TMPro;

public class AnimatedTextLetters : MonoBehaviour
{
    private TMP_Text animatedText;

    private float waveHeight = 15.0f;
    private float animationTime = 5.0f;

    private bool canAnimate = true;

    private void Start()
    {
        animatedText = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (canAnimate)
        {
            StartCoroutine(TMProTextAnimationDelay());
        }
    }

    /// <summary>
    /// Coroutine responsible for activate method with text animation in certain time
    /// </summary>
    /// <returns></returns>
    private IEnumerator TMProTextAnimationDelay()
    {
        TMProTextAnimation();
        yield return new WaitForSeconds(animationTime);
        canAnimate = false;
    }

    /// <summary>
    /// Method responsible for text animation after player lose game
    /// </summary>
    private void TMProTextAnimation()
    {
        animatedText.ForceMeshUpdate();
        TMP_TextInfo animatedTextInfo = animatedText.textInfo;

        for (int i = 0; i < animatedTextInfo.characterCount; i++)
        {
            TMP_CharacterInfo animatedCharacterInfo = animatedTextInfo.characterInfo[i];
            Vector3[] verts = animatedTextInfo.meshInfo[animatedCharacterInfo.materialReferenceIndex].vertices;
            int vertsLength = verts.Length / animatedTextInfo.characterCount;
            for (int j = 0; j < vertsLength; j++)
            {
                var originalVert = verts[animatedCharacterInfo.vertexIndex + j];
                verts[animatedCharacterInfo.vertexIndex + j] = originalVert + new Vector3(0.0f, Mathf.Sin(Time.time * 2 + originalVert.x) * waveHeight, 0.0f);
            }
        }

        for (int k = 0; k < animatedTextInfo.meshInfo.Length; k++)
        {
            TMP_MeshInfo singleMeshInfo = animatedTextInfo.meshInfo[k];
            singleMeshInfo.mesh.vertices = singleMeshInfo.vertices;
            animatedText.UpdateGeometry(singleMeshInfo.mesh, k);
        }
    }
}

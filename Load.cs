using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Load : MonoBehaviour
{

    public Slider cube;
    public Slider colors;
    public GameObject CubeCreator;
    int h;
    int l;
    bool couleur;
    bool fini = false;
    int nb;
    int tour = 0;
    int nombre = 0;
    int nombreT = 0;
    private LineRenderer lineRenderer;
    Color[] couleurs = { Color.red, Color.yellow, Color.blue, Color.green, Color.magenta, Color.gray, Color.cyan};

    bool end;




    public void Spawn()
    {
        //instanciation des liens et des blocks.
        int[,] liens = new int[(int)cube.value, (int)cube.value];
        GameObject[] block = new GameObject[(int)cube.value];
        //varaible permettant de voir si tous les blocks on un lien.
        int[] check = new int[(int)cube.value];

        //Remplir les cases de liens avec des 0.
        for (int i = 0; i < (int)cube.value; i++)
        {
            check[i] = 0;
            for (int h = 0; h < (int)cube.value; h++)
            {
                liens[h, i] = 0;
                liens[i, h] = 0;
            }
        }



        //Ajouter des liens aléatoires.
        for (int i = 0; i < cube.value;)
        {
            h = Random.Range(0, (int)cube.value);
            l = Random.Range(0, (int)cube.value);

            if (h == l)
            {
                liens[h, l] = 0;
                liens[l, h] = 0;
            }

            else if (liens[h,l] == 1)
            {

            }

            else if(check[h] >= 2)
            {
                i++;
            }

            else
            {
                liens[h, l] = 1;
                liens[l, h] = 1;
                check[h]++;
                nombre++;
                i++;
            }


        }


        //Assurer que tous les blocks sont reliés
        for (int i = 0; i < cube.value; i++)
        {
            if (check[i] != 1)
            {
                int random = Random.Range(0, (int)cube.value);

                while (end != true)
                {
                    random = Random.Range(0, (int)cube.value);

                    if (random == i)
                    {

                    }

                    else
                    {
                        end = true;
                    }
                }

                liens[i, random] = 1;
                liens[random, i] = 1;
                check[i] = 1;
                nombre++;
            }

        }

        //Faire appairaitre le nombres de block nécéssaire.
        for (int i = 0; i < block.Length; i++)
        {
            block[i] = (GameObject.Instantiate(CubeCreator, new Vector3(Random.Range(-50,50), Random.Range(-25,25), 0), transform.rotation));
            block[i].GetComponent<Renderer>().material.color = couleurs[Random.Range(0, (int)colors.value)];
        }


        //Faire appairaitre une ligne pour les liens entre les blocks.
        for (int p = 0; p < (int)cube.value; p++)
        {
            for (int o = 0; o < (int)cube.value; o++)
            {
                if (liens[p, o] == 1)
                {

                    lineRenderer = new GameObject().AddComponent<LineRenderer>();
                    lineRenderer.startWidth = 0.1f;
                    lineRenderer.endWidth = 0.1f;

                    lineRenderer.SetPosition(0, block[p].transform.position);
                    lineRenderer.SetPosition(1, block[o].transform.position);
                    
                }

                else
                {
                    
                }
            }
        }


        //Changer les couleurs.
        while (fini != true)
        {
            try
            {
                nombreT = 0;

                for (int i = 0; i < cube.value; i++)
                {
                    couleur = false;
                    nb = 0;
                    while (couleur != true)
                    {
                        for (int k = 0; k < cube.value; k++)
                        {
                            try
                            {
                                if (liens[i, k] == 1 && block[i].GetComponent<Renderer>().material.color != block[k].GetComponent<Renderer>().material.color && nb != check[i])
                                {
                                    nb++;
                                    nombreT++;
                                }

                                else if (liens[i, k] == 0)
                                {


                                }

                                else if (nb == check[i])
                                {
                                    couleur = true;
                                }

                                else
                                {
                                    block[k].GetComponent<Renderer>().material.color = couleurs[Random.Range(0, (int)colors.value)];
                                    nb = 0;
                                    Debug.Log("fefe");
                                    nombreT = 0;
                                }
                            }

                            catch
                            {
                                liens[i, k] = 0;
                                liens[k, i] = 0;
                                nb = 0;
                                check[i]--;
                                nombreT = 0;
                                couleur = true;
                            }


                        }
                    }
                }

                if (tour == 600 || nombreT == nombre)
                {
                    fini = true;
                }

                else
                {
                    tour++;
                    Debug.Log("Boucle");
                    fini = false;
                }
            }

            catch
            {
                Debug.Log("Une erreur s'est produite.");
                fini = true;

            }
           
        }

    }


    public void delete()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}

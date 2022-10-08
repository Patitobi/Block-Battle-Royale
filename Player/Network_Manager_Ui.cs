using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using System.Net;
using Unity.Netcode.Transports.UTP;
using System;
public class Network_Manager_Ui : MonoBehaviour
{
    //[SerializeField] private Button serverBtn;
    [SerializeField] private Button hostBtn;
    [SerializeField] private Button clientBtn;
    [SerializeField] private InputField InputIP;
    [SerializeField] private GameObject networkmanager;
    public static string IPV4;
    public static string ConnectIPV4;

    void Awake() {
        clientBtn.gameObject.SetActive(false);
        //Get Ip to display on UI
        //get local ip
        string hostname = Dns.GetHostName();
        IPV4 = Dns.GetHostEntry(hostname).AddressList[1].ToString(); //Index 1 weil 0 ipv6 scheinbar ist

        //Host Button
        hostBtn.onClick.AddListener(() => {
            //Menü Schließen
            hostBtn.gameObject.SetActive(false);
            clientBtn.gameObject.SetActive(false);
            InputIP.gameObject.SetActive(false);
            Debug.Log("Now Hosting on" + IPV4);
            //Setze auf eigene Ip
            networkmanager.GetComponent<UnityTransport>().ConnectionData.Address = IPV4;

            //Starte als Host
            NetworkManager.Singleton.StartHost();
        }); 

        //Client Button
        clientBtn.onClick.AddListener(() => {
            //Menü Schließen
            hostBtn.gameObject.SetActive(false);
            clientBtn.gameObject.SetActive(false);
            InputIP.gameObject.SetActive(false);
            //IP Eingabe
            Debug.Log("Now joining" + ConnectIPV4);
            //Setze auf eigene Ip
            networkmanager.GetComponent<UnityTransport>().ConnectionData.Address = ConnectIPV4;

            //Connect
            NetworkManager.Singleton.StartClient();
        });
    }

    public void IPAdressentered(string IP){
        if(IP != null && IP.Length > 8){
            clientBtn.gameObject.SetActive(true);
            ConnectIPV4 = IP;
        }else Debug.Log("Invalid Ip");
    }
}

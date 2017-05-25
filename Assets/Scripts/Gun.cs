using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Threading;

public class Gun : MonoBehaviour
{
	public Rigidbody2D rocket;				// Prefab of the rocket.
	public float speed = 20f;				// The speed the rocket will fire at.


	private PlayerControl playerCtrl;		// Reference to the PlayerControl script.
	private Animator anim;					// Reference to the Animator component.


	Thread receiveThread;
	UdpClient client;
	public int port = 26000; // DEFAULT UDP PORT !!!!! THE QUAKE ONE ;)
	string strReceiveUDP = "";
	string LocalIP = String.Empty;
	string hostname;

	public void Start()
	{
		Application.runInBackground = true;
		init();
	}

	// init
	private void init()
	{
		receiveThread = new Thread( new ThreadStart(ReceiveData));
		receiveThread.IsBackground = true;
		receiveThread.Start();
		hostname = Dns.GetHostName();
		IPAddress[] ips = Dns.GetHostAddresses(hostname);
		if (ips.Length > 0)
		{
			LocalIP = ips[0].ToString();
			Debug.Log(" MY IP : "+LocalIP);
		}
	}
	
	private  void ReceiveData()
	{
		client = new UdpClient(port);
		while (true)
		{
			try
			{
				IPEndPoint anyIP = new IPEndPoint(IPAddress.Broadcast, port);
				byte[] data = client.Receive(ref anyIP);
				strReceiveUDP = Encoding.UTF8.GetString(data);
				// ***********************************************************************
				// Simple Debug. Must be replaced with SendMessage for example.
				// ***********************************************************************
				Debug.Log(strReceiveUDP);
				// ***********************************************************************
			}
			catch (Exception err)
			{
				print(err.ToString());
			}
		}
	}
	

	
	
	public string UDPGetPacket()
	{
		return strReceiveUDP;
	}
	
	void OnDisable()
	{
		if ( receiveThread != null) receiveThread.Abort();
		client.Close();
	}



	void Awake()
	{
		// Setting up the references.
		anim = transform.root.gameObject.GetComponent<Animator>();
		playerCtrl = transform.root.GetComponent<PlayerControl>();
	}


	void Update ()
	{
		// If the fire button(left click on mouse) is pressed... or user issued voice command "SHOOT".
		if(Input.GetButtonDown("Fire1") || strReceiveUDP.Equals ("shoot") )
		{    strReceiveUDP = "";  // show that it donnt keeps on shooting.
			// ... set the animator Shoot trigger parameter and play the audioclip.
			anim.SetTrigger("Shoot");
			GetComponent<AudioSource>().Play();

			// If the player is facing right...
			if(playerCtrl.facingRight)
			{
				// ... instantiate the rocket facing right and set it's velocity to the right. 
				Rigidbody2D bulletInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
				bulletInstance.velocity = new Vector2(speed, 0);
			}
			else
			{
				// Otherwise instantiate the rocket facing left and set it's velocity to the left.
				Rigidbody2D bulletInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0,0,180f))) as Rigidbody2D;
				bulletInstance.velocity = new Vector2(-speed, 0);
			}
		}
	}
}

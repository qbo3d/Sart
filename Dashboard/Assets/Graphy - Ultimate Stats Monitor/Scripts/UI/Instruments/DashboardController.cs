using AdultLink;
using IndiePixel;
using System.Collections;
using System.Collections.Generic;
using Tayx.Graphy.Rpm;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.UI;

public class DashboardController : MonoBehaviour
{

    SerialPort myPort;     [SerializeField] private Dropdown myDrop;
    [SerializeField] private Button conectarBtn;     [SerializeField] private Button desconectarBtn;
    [SerializeField] private Text matrix;
    bool isConnected = false;

    public G_RpmMonitor EGT;
    public G_RpmMonitor CGT;
    public IP_Airplane_Altimeter Altimeter;
    public IP_Airplane_Airspeed Airspeed;
    public IP_Airplane_Tachometer Tachometer;
    public SpeedBar Fuel;
    public SpeedBar Batt;


    public long t;
    public bool M_ER;//AKA: Mode_ER
    public int check_GOV; //AKA: Auto_check
    public int Mode_ER;
    public float EGT_data;// 125.7° = 1257
    public float CGT_data;
    public float rpm;
    public float altm;//2500.7m = 25007
    public float pitot;//2500.7m = 25007
    public bool Cck_N; //12.0h = 120
    public float Cck;
    public float Cck_L;
    public float Aj_F;//AKA: Ajuste_F
    public float check;
    public float PWM1;
    public float PWM2;
    public float Gal;//6.51 Gal = 651
    public float Is;//10.250A = 10250
    public float Vs;//6.40V = 640
    public float mAh;
    public float TempC;//22.4° = 224
    public float horas;
    public int Mili_temp;
    public float numAltR;
    public float PWM_H;
    public float PWM_L;
    public float Pwm_Rpm;
    public float c_alarm;

    //Split
    string n_s = "EGT = 21.34";
    //string name;

    // Use this for initialization
    void Start()
    {

        //Split
        string[] splitArray = n_s.Split(char.Parse("=")); //Here we assing the splitted string to array by that char
        name = splitArray[0]; //Here we assign the first part to the name

        RefreshPorts();

        t = 0;
        Mode_ER = 2;
        check_GOV = 3;
        EGT_data = 21;
        CGT_data = 21;
        rpm = 0;
        altm = 3000;
        pitot = 40;
        numAltR = 1;
        Cck = 1;
        Cck_L = 1;
        Aj_F = 0;
        check = 0;
        PWM_H = 2037;
        PWM_L = 1642;
        Gal = 1;
        Is = 0;
        Vs = 1;
        mAh = 1;
        Pwm_Rpm = 1000;
        c_alarm = 1;
    }

    // Update is called once per frame
    void Update()
    {
        string linea;
        string test = "";

        matrix.text += "\n18400\t1\t0\t20.4\t22.7\t0\t2485.8\t4\t2.5\t7.0\t0\t0.0\t2001\t1642\t1.75\t0.000\t8.74\t0.0040\t1936\t0\t29\t42\r";
        if (isConnected)         {             try             {
                linea = myPort.ReadLine();                 Debug.Log(linea);

                matrix.text += test;

                test = linea.Trim().Split(char.Parse("\t"))[0];

                if (linea.Trim().Split(char.Parse("="))[0].Equals("EGT"))
                {
                    EGT_data = float.Parse(linea.Split('=')[1]);
                }
                else if (linea.Trim().Split(char.Parse("="))[0].Equals("CGT"))
                {
                    CGT_data = float.Parse(linea.Split('=')[1]);
                }

                t = t + 100;
                //EGT_data = EGT_data < 50f ? EGT_data + Random.Range(0.01f, 0.05f) : 0;
                //CGT_data = CGT_data < 50f ? CGT_data + Random.Range(0.01f, 0.05f) : 0;
                rpm = rpm < 5000 ? rpm + Random.Range(1f,5f) : 0;
                altm = altm < 35000 ? altm + Random.Range(0.1f, 0.5f) : 0;
                pitot = pitot < 200 ? pitot + Random.Range(0.1f, 0.5f) : 0;
                numAltR = numAltR < 5 ? numAltR + Random.Range(0.1f, 0.5f) : 0;
                Cck = Cck < 7 ? Cck + Random.Range(0.1f, 0.5f) : 0;
                Cck_L = Cck_L < 7 ? Cck_L + Random.Range(0.1f, 0.5f) : 0;
                Aj_F = Aj_F < 2 ? Aj_F + Random.Range(0.1f, 0.5f) : 0;
                check = check < 2 ? check + Random.Range(0.1f, 0.5f) : 0;
                Gal = Gal < 10 ? Gal + Random.Range(0.001f, 0.005f) : 0;
                Is = Is < 10 ? Is + Random.Range(0.001f, 0.005f) : 0;
                Vs = Vs < 100 ? EGT_data + Random.Range(0.1f, 0.5f) : 0;
                mAh = mAh < 100 ? mAh + Random.Range(0.1f, 0.5f) : 0;
                Pwm_Rpm = Pwm_Rpm < 3000 ? Pwm_Rpm + Random.Range(0.1f, 0.5f) : 0;
                c_alarm = c_alarm < 100 ? c_alarm + Random.Range(0.1f, 0.5f) : 0;

                EGT.ReservedRpm = EGT_data;
                CGT.ReservedRpm = CGT_data;
                Altimeter.CurrentMSL = altm;
                Airspeed.MPH = pitot;
                Tachometer.CurrentRPM = rpm;
                Fuel.fillPercentage = Gal / 10;
                Batt.fillPercentage = Is / 10;
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex.ToString());
            }
        }
    }

    public void RefreshPorts()     {         List<string> ports = new List<string> { };         foreach (string port in SerialPort.GetPortNames())         {             ports.Add(port);         }         myDrop.ClearOptions();         myDrop.captionText.fontSize = 25;         myDrop.AddOptions(ports);     }

    public void AttemptConnection()     {         //print("Trying to connect to port " + myDrop.captionText.text);"/dev/tty.usbserial-AH01466S"         //myDrop.captionText.text, 57600, Parity.None, 8, StopBits.One         //string,int,int,System.IO.Ports.Parity,System.IO.Ports.StopBits,bool,bool,System.IO.Ports.Handshake,int,int,int,int         myPort = new SerialPort(myDrop.captionText.text, 57600,Parity.None,8,StopBits.One);         //myPort = new SerialPort("tty.usbserial-AH01466S", 57600);         try         {             myPort.Open();             myPort.ReadTimeout = 10;             desconectarBtn.gameObject.SetActive(true);
            isConnected = true;         }         catch (System.Exception ex)         {
            Debug.LogError(ex.ToString());         }     }

    public void Disconnect()     {         myPort.Close();         conectarBtn.gameObject.SetActive(true);         desconectarBtn.gameObject.SetActive(false);         isConnected = false;     } 
}

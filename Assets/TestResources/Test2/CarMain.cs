using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TestResources.Test2
{
    public class CarMain : MonoBehaviour
    {
        [SerializeField] private GameObject[] carObjects;
        private List<Material> _materials = new List<Material>();

        [Header("UI Component")] 
        [SerializeField] private Text descriptionTxt;
        public RawImage rawImagePrefab;
        public Transform parentTransform;
    
        //Data
        [SerializeField] private CarData _carsData;

        private void Start()
        {
            foreach (var car in carObjects)
            {
                _materials.Add(car.GetComponent<MeshRenderer>().material);
            }
        
            for (int i = 1; i < carObjects.Length; i++)
            {
                carObjects[i].SetActive(false);
            }

            LoadData();
        
            ChangeDescription(0);
        }

        public void ChangeCar()
        {
            for (int i = 0; i < carObjects.Length; i++)
            {
                if (carObjects[i].activeSelf)
                {
                    int nextCarIndex = (i + 1) % carObjects.Length;
                    carObjects[i].SetActive(false);
                    carObjects[nextCarIndex].SetActive(true);
                    ChangeRandomColor(nextCarIndex);
                    ChangeDescription(nextCarIndex);
                    break;
                }
            }
        }

        private void ChangeDescription(int indexCar)
        {
            descriptionTxt.text = $"{_carsData.cars[indexCar].name} \n \n" +
                                  $"{_carsData.cars[indexCar].description} \n \n" +
                                  "Gallery";
            DisplayImage(indexCar);
        }

        private void ChangeRandomColor(int indexCar)
        {
            Color randomColor = new Color(Random.value, Random.value, Random.value);
            _materials[indexCar].color = randomColor;
        }

        private void LoadData()
        {
            TextAsset jsonFile = Resources.Load<TextAsset>("car_information");

            if (jsonFile != null)
            {
                _carsData = JsonUtility.FromJson<CarData>(jsonFile.text);
            
                foreach (Car car in _carsData.cars)
                {
                    car.carImage = Resources.LoadAll<Texture2D>($"Images/{car.name}");
                }
            }
            else
            {
                Debug.LogError("File not found in Resources folder.");
            }
        }

        private void DisplayImage(int indexCar)
        {
            if (rawImagePrefab == null || parentTransform == null)
            {
                Debug.LogError("RawImage prefab or parent transform is not assigned!");
                return;
            }
        
            foreach (Transform child in parentTransform)
            {
                Destroy(child.gameObject);
            }
        
            for (int i = 0; i < _carsData.cars[indexCar].carImage.Length; i++)
            {
                RawImage newRawImage = Instantiate(rawImagePrefab, parentTransform);
                newRawImage.texture = _carsData.cars[indexCar].carImage[i];
            }
        }
    }

    [System.Serializable]
    public class Car
    {
        public string name;
        public string description;
        public Texture2D[] carImage;
    }

    [System.Serializable]
    public class CarData
    {
        public Car[] cars;
    }
}
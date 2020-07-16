using UnityEngine;
using UnityEngine.UI;
using Stellarium;
using Stellarium.Services;
using UnityEngine.EventSystems;

public class StellariumUI : MonoBehaviour {

    [SerializeField]
    InputField hourInput, minuteInput, secondInput;
    [SerializeField]
    Slider timeSlider;
    [SerializeField]
    Dropdown monthDropdown;
    [SerializeField]
    InputField dayInput, yearInput;

    Status status;
    bool usingTimeSlider = false;

    void OnEnable() {
        MainService.OnGotStatus += OnGotStatus;
        MainService.OnSetTime += OnSetTime;
    }

    char ValidateIntMinMax(string input,int index,char charToValidate,int min,int max) {
        int val = StringToInt(input+charToValidate);
        if(index>0 && (val < min || val > max)) {
            charToValidate = '\0';
        }
        return charToValidate;
    }

    public void GenerateSkybox() {
        StellariumServer.Instance.ScriptService.Run("skybox.ssc");
    }

    public void GetStatus() {
        StellariumServer.Instance.MainService.GetStatus(status.actionChanges.id, status.propertyChanges.id);
    }

    void Start() {
        Application.runInBackground = true;
        StellariumServer.Instance.MainService.GetStatus();
    }

    void OnGotStatus(Status result) {
        status = result;
        CustomDateTime stellariumDateTime = new CustomDateTime(result.time.jday);
        hourInput.text = stellariumDateTime.hour.ToString("D2");
        minuteInput.text = stellariumDateTime.minute.ToString("D2");
        secondInput.text = stellariumDateTime.second.ToString("D2");
        monthDropdown.value = stellariumDateTime.month-1;
        dayInput.text = stellariumDateTime.day.ToString();
        yearInput.text = (stellariumDateTime.year * (stellariumDateTime.year<0?-1:1)).ToString();
        SetTimeSlider();
    }

    private void OnSetTime() {
        if(!usingTimeSlider) {
            StellariumServer.Instance.MainService.GetStatus(status.actionChanges.id, status.propertyChanges.id);
        }
    }

    public void OnTimeSliderDown() {
        usingTimeSlider = true;
    }

    public void OnTimeSliderUp() {
        usingTimeSlider = false;
    }

    public void SetTimeInput() {
        int hour = (int)timeSlider.value;
        int minute = (int)((timeSlider.value - hour) * 60);
        int second = (int)((timeSlider.value -hour- (timeSlider.value - hour)) *60*60);
        hourInput.text = hour.ToString("D2");
        minuteInput.text = minute.ToString("D2");
        secondInput.text = second.ToString("D2");
    }

    public void SetTimeSlider() {
        if(!usingTimeSlider) {
            timeSlider.value = StringToInt(hourInput.text) + (StringToInt(minuteInput.text) / 60f) + (StringToInt(secondInput.text) / 60f / 60f);
        }
    }

    public void UpdateDateTime() {
        StellariumServer.Instance.MainService.SetTime(CustomDateTime.ToJulianDay(new CustomDateTime(
            StringToInt(yearInput.text),
            monthDropdown.value+1,
            StringToInt(dayInput.text),
            StringToInt(hourInput.text),
            StringToInt(minuteInput.text),
            StringToInt(secondInput.text),
            (CustomDateTime.Era)(StringToInt(yearInput.text)<0?0:1)
            )),
            0f
        );
    }

    int StringToInt(string str) {
        int result = 0;
        if(!int.TryParse(str, out result)) {
            Debug.LogError("Could not parse int");
            return result;
        }
        return result;
    }

    void OnDisable() {
        MainService.OnGotStatus -= OnGotStatus;
        MainService.OnSetTime -= OnSetTime;
    }

}

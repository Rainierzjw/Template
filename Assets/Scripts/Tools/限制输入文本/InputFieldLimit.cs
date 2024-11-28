/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称：InputFieldLimit
* 创建日期：2023-03-23 14:54:26
* 作者名称：韦伟
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Com.Rainier.Buskit3D;
using UnityEngine.UI;


/// <summary>
/// 
/// </summary>
public class InputFieldLimit : MonoBehaviour 
{
    InputField inputField;
    [SerializeField] int maxLength = 10;
    [SerializeField] int maxIntLength = 6;
    [SerializeField] int maxFloatLength = 6;
    [SerializeField] int maxDecimalPlaces = 2;
    [SerializeField] float minValue = 0;
    [SerializeField] float maxValue = 100;

    void Start()
    {
        inputField = GetComponent<InputField>();
        inputField.onEndEdit.AddListener(OnEndEdit);
    }
    void OnEndEdit(string text)
    {
        // 判断长度
        if (text.Length > maxLength)
        {
            text = text.Substring(0, maxLength);
        }
        // 转换为数字
        float result;
        bool isNumber = float.TryParse(text, out result);
        if (isNumber)
        {
            // 判断长度
            string[] parts = text.Split('.');
            if (parts[0].Length > maxIntLength)
            {
                parts[0] = parts[0].Substring(0, maxIntLength);
            }
            if (parts.Length > 1 && parts[1].Length > maxFloatLength)
            {
                parts[1] = parts[1].Substring(0, maxFloatLength);
            }
            // 判断小数位数
            if (parts.Length > 1 && parts[1].Length > maxDecimalPlaces)
            {
                parts[1] = parts[1].Substring(0, maxDecimalPlaces);
            }
            // 判断数值范围
            float value = float.Parse(string.Join(".", parts));
            if (value < minValue) value = minValue;
            if (value > maxValue) value = maxValue;
            text = value.ToString();
        }
        inputField.text = text;
    }

}
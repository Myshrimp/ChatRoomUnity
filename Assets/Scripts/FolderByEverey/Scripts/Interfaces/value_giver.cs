

//给值者   通过指针来获得值，最重要的就是给滚动条来多选
public interface value_giver {
    //int 指针 giver_pointer;
    //float[] 值 giver_values;
    //string[] 字符串值 giver_strs;
    string toString();//以字符串组合返回
    //string str_front,str_back;//自定义字符串组合
    //bool use_value=false;//使用值还是字符串参与组合,默认字符串

    float getValue();//返回值
    string getStr();//返回字符串
    void uiToPointer(float i);//把ui值转换成指针

    //bool isUsing =true; //是否启用，可能被禁用


}

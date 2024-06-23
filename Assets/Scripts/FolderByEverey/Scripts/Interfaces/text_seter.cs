
//自动绑定给值者，把他的字符或者值输出给一个文本ui

public interface text_seter
{
    //TextMeshProUGUI text; 被输出的文本ui
    //bool isUseValue=false 是否使用值,默认使用文本
    //自动绑定给值者，不考虑性能，输出文本写在帧事件,若禁用，则说明“被禁用”
    void setText();
}
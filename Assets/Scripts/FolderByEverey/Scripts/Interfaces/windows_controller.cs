
//控制某窗口的开关 默认点击打开，再点击关闭，通过activeself判断
public interface windows_controller
{
    //Gameobject close,open;被关闭的，被打开的
    //bool isUsing=false;//是否在使用
    void use(); //应用一次
    void self_checking();//自检，如果窗口已经对应关和开，自己对应状态来防止bug
  
}

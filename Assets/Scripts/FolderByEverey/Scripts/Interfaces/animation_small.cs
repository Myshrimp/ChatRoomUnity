//自定义小动画，轻便快捷
public interface animation_small
{
    //bool isUsing 是否正在启用
    //float once_time;  播放一个画面需要的时间
    //int times;  播放次数
    //bool looping; 是否循环
    //Sprite[] sprites; 播放列表
    //bool will_destroy; 次数归零后并且不循环，是否销毁
    //辅助计时计数的不写了
    void play();
    void stop();
}

# caleb-UI

1. templated control. 작성 및  avaloniaUI control 소스에서 살펴볼것 button 이나 기타 등등.
2. mousebutton down 및 up 관련 event 관련 해서 AddHandler 말고 using 구문 넣어서 사용하는 방법 살펴볼 것. 
추가적으로 확인해야 하는 것이 만든 클래스를 통해 상속받았을때 이상 유무를 확인해야함.
 -> avalonia.controls 에서 button 의 경우는 addHandler 로 event 핸들러를 추가하는 방식을 취함. 이 부분은 심사숙고 하자.
3. 클래스 라이브러리로 제작. cs 파일과 xaml 연계 되는 부분. 지금 살펴본봐로는 cs 만으로도 xaml 로 대체가 가능한 형태임.

## TODO List

1. 간단한 클래스 라이브러를 제작해서 이걸 가지고 xaml 방식으로 제작하는 방법과 cs 로 생성하는 방법 두가지를 테스트 해본다.
2. 상기 templated control 제작하는 부분과 연계해서 한번 살펴볼 필요는 있음.

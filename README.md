# MMO_Unity

게임 제작을 하기위한 프레임워크 설계.
후에 게임을 제작할때 참고하기 위한 프로젝트입니다.

## Manager 코드 구현 내용

### Managers.cs

모든 매니저를 갖고있는 매니저 싱글톤 패턴으로 구현하여 다른 매니저도 접근 할 수 있다.

### InputManager.cs

디자인 패턴인 옵저버 패턴을 이용해서만든 InputManager 사용자의 입력이 들어오면 KeyAction에 등록된 함수를 실행시킨다.

### ResourceManager.cs

프리팹화 시킨 오브젝트를 불러오는 방법중에 하나로 유니티에서 Resources 폴더를 만들고 그 폴더를 통하여 오브젝트를 불러오는 방법이 있는데 그 방법으로 리소스를 불러오는 역할을 하는 Manager이다.

### UIManager.cs

PopupUI : 게임 진행 시 팝업으로 뜨고 사라지는 Dialog식 UI들
SceneUI : 게임 진행 시 설정창이나 인벤토리와 같은 UI

PopupUI 활성화 시 순서(sorting order : 화면에 보여지는 순서 )를 관리해준다. UI를 생성 혹은 삭제등을 해주는 Manager이다.

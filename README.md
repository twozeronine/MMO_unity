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

### SceneManagerEx.cs

> 유니티에 이미 SceneManager라는 이름이 예약되있기 때문에 이름을 조금 바꾼것이다.

씬을 관리하는 매니저이다.
기존에 존재하던 SceneManager의 기능들을 매핑하여 다시 구현을 하여서 하나의 Manager에서 관리하도록 해주었습니다.

### SoundManager.cs

사운드를 관리하는 매니저이다.

사운드를 내는 AudioSource같은 경우 특정 오브젝트에 붙이게되면 오브젝트가 비활성화되거나 삭제될때 더이상 소리를 못내거나 접근할 수 없기 때문에 사운드를 관리할 매니저가 필요하다.

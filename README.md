# Slaking (게을킹)

2024년 봄 학기 숭실대학교 글로벌미디어학부 졸업 프로젝트로 제작한 인터랙티브 공연형 작품입니다. VR, 피라미드형 홀로그램, 모션캡처를 결합해 관객이 가상 아이돌의 무대 안으로 들어가고, 무대 밖의 관객도 홀로그램과 화면을 통해 같은 공연을 경험하도록 구성했습니다.

## 프로젝트 소개

Slaking은 단순히 영상을 재생하는 전시물이 아니라, 관객의 착용 상태와 입력, 모션캡처 캐릭터, 무대 영상, 음악 싱크, 웹캠 화면을 하나의 공연 흐름으로 연결하는 Unity 기반 체험형 콘텐츠입니다.

체험자는 HMD를 착용하고 VR 공간에서 무대와 캐릭터를 바라보며 진행합니다. 외부 관객은 피라미드형 홀로그램 장치와 별도 디스플레이를 통해 캐릭터, 무대 연출, 안내 화면을 함께 볼 수 있습니다. 공연 중에는 영상, 음악, 조명, 캐릭터 애니메이션이 상태 전환에 따라 자동으로 바뀌며, 후반부에는 웹캠을 통해 관객과 캐릭터가 마주 보는 커뮤니케이션 장면으로 이어집니다.

## 시연 영상

- [Slaking 시연 영상](https://www.youtube.com/watch?v=Of74VGpOiYA)

## 주요 기능

- VR HMD 기반 체험 흐름
- 피라미드형 홀로그램을 위한 전/후/좌/우 렌더 텍스처 및 전용 카메라 구성
- Sony mocopi 기반 모션캡처 수신 및 캐릭터 적용
- VRM/3D 캐릭터 모델을 활용한 버추얼 아이돌 무대
- FMOD 타임라인 콜백을 활용한 비트/마커 기반 음악 싱크
- 튜토리얼 영상, 공연 영상, 웹캠 화면 전환
- 곡 정보 JSON을 기반으로 한 노트/응원 타이밍 데이터 파싱
- Title, Stage, End 씬을 잇는 상태 기반 공연 진행
- 다중 디스플레이 출력 지원

## 체험 흐름

1. 타이틀 화면에서 시작 입력을 기다립니다.
2. 일정 시간 입력이 없으면 데모 공연이 재생됩니다.
3. 체험자가 HMD를 착용하고 시작하면 무대 씬으로 이동합니다.
4. VR 위치를 리센터링한 뒤 튜토리얼을 진행합니다.
5. 곡 선택 연출 이후 공연 영상과 음악이 재생됩니다.
6. 공연 종료 후 웹캠 화면으로 전환되어 관객과 커뮤니케이션합니다.
7. 앙코르 요청 또는 종료 연출을 거쳐 엔딩 씬으로 이동합니다.

## 기술 스택

- Unity 2022.3.13f1
- XR Interaction Toolkit 2.5.3
- Oculus XR Plugin
- FMOD for Unity
- Sony mocopi Receiver
- uOSC / EVMC4U
- UniVRM / VRM
- TextMeshPro
- Unity VideoPlayer / WebCamTexture

## 프로젝트 구조

```text
Assets/
  Code/Script/          핵심 시스템, 음악, 노트, HMD, 홀로그램 스크립트
  Code/Plugins/         FMOD, MocopiReceiver, uOSC, UniVRM 등 외부 플러그인
  Prefab/               VR, UI, Hologram, Flat, Model 프리팹
  Scenes/               Title, Stage, Loading, End, Demo 씬
  Art/                  캐릭터, 무대, 영상, 모션, 이미지, 렌더 텍스처 리소스
  Resources/SongInfo/   곡 메타데이터 및 노트 타이밍 JSON
  StreamingAssets/      음악, 효과음, FMOD bank 파일
```

## 실행 방법

### Unity Editor에서 실행

1. Unity Hub에서 `2022.3.13f1` 버전으로 프로젝트를 엽니다.
2. XR 장비, 웹캠, mocopi 수신 환경이 필요한 경우 각 장치를 먼저 연결합니다.
3. `Assets/Scenes/Loading.unity` 또는 `Assets/Scenes/Title.unity`를 열고 Play합니다.
4. HMD와 외부 디스플레이가 연결되어 있다면 다중 디스플레이 출력이 활성화됩니다.

### 빌드 파일 실행

빌드 결과물은 `Build/Main-Build/senier_project.exe`에 포함되어 있습니다.

## 조작 방법

- `Space`: 시작, 가이드/공연 진행, 일부 장면 전환
- `R`: VR 위치 리센터링 후 다음 단계로 진행
- `W`: 웹캠/공연 화면 전환 테스트
- `S`: 응원봉 흔들기 테스트

## 주요 씬

- `Loading`: 다음 씬 로드 및 디스플레이 초기화
- `Title`: 타이틀, 데모 공연, 시작 입력 처리
- `Stage`: 튜토리얼, 곡 선택, 공연, 웹캠 커뮤니케이션
- `End`: 엔딩 화면 및 종료 입력 처리
- `Demo`: 캐릭터/무대 데모 확인용 씬

## 구현 메모

- `GameManager`는 작품 전체의 상태를 `TITLE`, `DEMO`, `WEARING`, `TUTORIAL`, `SELECT_MUSIC`, `GAME`, `COMMUNICATION`, `RESULT`, `REQUEST_ENCORE`, `GAMEOVER`로 관리합니다.
- `StageScreenSwitcher`는 공연 영상, 튜토리얼 영상, 정지 이미지, 웹캠 텍스처를 하나의 무대 화면에 전환합니다.
- `MusicManager`는 FMOD 타임라인의 beat/marker 콜백을 받아 조명 및 연출 타이밍과 연결합니다.
- `SetHologramCamera`와 렌더 텍스처 리소스는 피라미드형 홀로그램 출력에 필요한 시점 구성을 담당합니다.
- `switchControl`은 mocopi 실시간 모션과 사전 제작 애니메이션을 전환하며 캐릭터 공연을 구성합니다.

## 팀

### 김종겸

강릉에서 태어났습니다.

### 이용섭

집에 가고 싶다.

### 고민성

(자기 소개)

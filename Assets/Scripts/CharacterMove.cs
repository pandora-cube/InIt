﻿using UnityEngine;
using System.Collections;

public class CharacterMove : MonoBehaviour {
    public float moveSpeed = 3f;    // 이동 속도
    public int animSpeed = 3;       // 애니메이션 재생 속도
    public Sprite[] runningSprites; // 달리기 스프라이트
    
    float backgroundPosX;           // 배경의 X좌표
    float backgroundPosY;           // 배경의 Y좌표
    float backgroundWidth;          // 배경의 두께
    float backgroundHeight;         // 배경의 높이
    float cameraWidth;              // 화면의 두께
    float cameraHeight;             // 화면의 높이
    Sprite standingSprite;          // 정지상태 스프라이트
    bool canmove = true;            // 캐릭터 이동 가능 여부
    int animIndex;                  // 애니메이션 재생 인덱스
    
	void Start() {
        // 초기화
        GetBackgroundStatus();
        cameraHeight = 2f * Camera.main.orthographicSize;
        cameraWidth = cameraHeight * Camera.main.aspect;
        standingSprite = GetComponent<SpriteRenderer>().sprite;
	    animIndex = 0;
        
        // 디버깅 메시지 출력
        Debug.Log(string.Format("Background: {0}, {1}", backgroundWidth, backgroundHeight));
        Debug.Log(string.Format("Camera: {0}, {1}", cameraWidth, cameraHeight));
	}
	
	void Update() {
        /*
         *  CharacterMove.Update()
         *      캐릭터 이동
         *      화면 이동
         */
        
        // 이동 불가능한 경우
        if(!canmove)
            return;

        // 이동중 여부 체크 변수
        bool isrunning = false;
        // 캐릭터 스프라이트
        SpriteRenderer charspr = GetComponent<SpriteRenderer>();

        /* 캐릭터 이동 */
        Vector3 scale = transform.localScale;
        scale.x = -Mathf.Abs(scale.x);
        transform.localScale = scale;

        // 좌향 이동
        if(Input.GetKey(KeyCode.LeftArrow)) {
            isrunning = true;
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            // 방향 처리
            charspr.flipX = false;
        }
        // 우향 이동
        if(Input.GetKey(KeyCode.RightArrow)) {
            isrunning = true;
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            // 방향 처리
            charspr.flipX = true;
        }
        // 상향 이동
        if(Input.GetKey(KeyCode.UpArrow)) {
            isrunning = true;
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }
        // 하향 이동
        if(Input.GetKey(KeyCode.DownArrow)) {
            isrunning = true;
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }
        else
            // 스프라이트 초기화
            charspr.sprite = standingSprite;
        
        // 이동중인 경우
        if(isrunning) {
            // 달리기 애니메이션 인덱스 처리
            animIndex++;
            if(animIndex/animSpeed >= runningSprites.Length)
                animIndex = 0;
            // 달리기 스프라이트 이미지로 전환
            charspr.sprite = runningSprites[animIndex/animSpeed];
        }
        
        /* 화면 이동 */
        Vector3 charpos = charspr.transform.position;                   // 캐릭터의 좌표
        Vector3 campos = Camera.main.transform.position;                // 화면의 좌표
        Vector3 campos_moved = campos;                                  // 화면의 이동될 좌표
        float left = charpos.x - backgroundPosX - cameraWidth/2f;       // 화면의 왼쪽 끝
        float right = charpos.x - backgroundPosX + cameraWidth/2f;      // 화면의 오른쪽 끝
        float bottom = charpos.y - backgroundPosY - cameraHeight/2f;    // 화면의 아랫쪽 끝
        float top = charpos.y - backgroundPosY + cameraHeight/2f;       // 화면의 윗쪽 끝
        // 맵의 왼쪽 끝과 캐릭터 x좌표의 거리
        float xdis_left = backgroundPosX - backgroundWidth/2f - charpos.x;
        // 맵의 오른쪽 끝과 캐릭터 x좌표의 거리
        float xdis_right = backgroundPosX + backgroundWidth/2f - charpos.x;
        // 맵의 아랫쪽 끝과 캐릭터 y좌표의 거리
        float ydis_bottom = backgroundPosY - backgroundHeight/2f - charpos.y;
        // 맵의 윗쪽 끝과 캐릭터 y좌표의 거리
        float ydis_top = backgroundPosY + backgroundHeight/2f - charpos.y;
        
        // 화면의 좌우 영역이 맵 안쪽에 있는 경우
        if(left >= -backgroundWidth/2f && right <= backgroundWidth/2f)
            campos_moved.x = charpos.x;
        // 맵 왼쪽으로 벗어난 경우
        else if(Mathf.Abs(xdis_left) < Mathf.Abs(xdis_right))
            campos_moved.x = backgroundPosX - backgroundWidth/2f + cameraWidth/2f;
        // 맵 오른쪽으로 벗어난 경우
        else
            campos_moved.x = backgroundPosX + backgroundWidth/2f - cameraWidth/2f;
            
        // 화면의 상하 영역이 맵 안쪽에 있는 경우
        if(bottom >= -backgroundHeight/2f && top <= backgroundHeight/2f)
            campos_moved.y = charpos.y;
        // 맵 아랫쪽으로 벗어난 경우
        else if(Mathf.Abs(ydis_bottom) < Mathf.Abs(ydis_top))
            campos_moved.y = backgroundPosY - backgroundHeight/2f + cameraHeight/2f;
        // 맵 윗쪽으로 벗어난 경우
        else
            campos_moved.y = backgroundPosY + backgroundHeight/2f - cameraHeight/2f;
        // 화면 좌표를 캐릭터 좌표와 동기화
        Camera.main.transform.position = new Vector3(campos_moved.x, campos_moved.y, campos_moved.z);
	}

    void OnCollisionEnter2D(Collision2D col) {
        // 캐릭터 스프라이트
        SpriteRenderer charspr = GetComponent<SpriteRenderer>();

        // 출입구 영역으로 들어온 경우
        if(col.gameObject.tag == "Entrance") {
            // 출입구 오브젝트의 이름을 Split하여 정보를 얻음
            // [0] 출입구 이름
            // [1] 충돌한 출입구의 방향 (Outside or Inside)
            string[] objInfo = col.gameObject.name.Split('/');
            // 반대편 출입구 이름
            string entrance = objInfo[0] + "/" + ((objInfo[1] == "Outside") ? "Inside" : "Outside");
            // 반대편 출입구 스프라이트
            SpriteRenderer other = GameObject.Find(entrance).GetComponent<SpriteRenderer>();

            // 반대편 출입구를 트리거로 처리
            GameObject.Find(entrance).GetComponent<BoxCollider2D>().isTrigger = true;
            // 캐릭터를 반대편 출입구로 이동
            charspr.transform.position = new Vector3(other.transform.position.x, other.transform.position.y, charspr.transform.position.z);

            // 맵 이동에 따른 배경 스프라이트 정보 갱신
            GetBackgroundStatus();
        }
        else if(col.gameObject.tag == "NPC")
            GameObject.Find("Dialogue UI").GetComponent<Dialogue>().ShowDialogue("거북이", "이곳에 내용이 출력됩니다.", false);
    }

    void OnTriggerExit2D(Collider2D col) {
        // 출입구 영역에서 벗어난 경우
        if(col.gameObject.tag == "Entrance") {
            // 트리거 처리 해제
            col.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }

    string GetPlayerArea() {
        // 캐릭터 좌표
        Vector3 chrPos = GetComponent<SpriteRenderer>().transform.position;

        foreach(SpriteRenderer spr in GameObject.FindObjectsOfType<SpriteRenderer>()) {
            string[] sprInfo = spr.name.Split('_');
            Vector3 sprPos = spr.transform.position;
            Vector3 sprSize = spr.bounds.size;

            // 캐릭터가 특정 배경 스프라이트의 영역 안에 있는 경우
            if(sprInfo[0] == "Background" && sprInfo[1].Length > 0
                && chrPos.x >= sprPos.x-sprSize.x/2f && chrPos.x <= sprPos.x+sprSize.x/2f
                && chrPos.y >= sprPos.y-sprSize.y/2f && chrPos.y <= sprPos.y+sprSize.y/2f)
                return spr.name;
        }
        return string.Empty;
    }

    void GetBackgroundStatus() {
        // 배경 스프라이트 선택자
        SpriteRenderer obj = GameObject.Find(GetPlayerArea()).GetComponent<SpriteRenderer>();

        // 배경 스프라이트의 좌표를 구함
        backgroundPosX = obj.transform.position.x;
        backgroundPosY = obj.transform.position.y;
        // 배경 스프라이트의 너비와 높이를 구함
        backgroundWidth = obj.bounds.size.x;
        backgroundHeight = obj.bounds.size.y;
    }
}
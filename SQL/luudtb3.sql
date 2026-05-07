select name from v$pdbs;
CREATE TABLE APP_SESSION (
    USERNAME     VARCHAR2(50) PRIMARY KEY,
    SESSION_ID   VARCHAR2(100),
    DEVICE_NAME  VARCHAR2(200),
    LOGIN_TIME   TIMESTAMP
);
CREATE OR REPLACE PROCEDURE SET_SESSION (
    p_username IN VARCHAR2,
    p_session  IN VARCHAR2,
    p_device   IN VARCHAR2
)
AS
BEGIN
    MERGE INTO APP_SESSION s
    USING (SELECT p_username username FROM dual) x
    ON (s.USERNAME = x.username)
    WHEN MATCHED THEN
        UPDATE SET SESSION_ID = p_session,
                   DEVICE_NAME = p_device,
                   LOGIN_TIME = SYSTIMESTAMP
    WHEN NOT MATCHED THEN
        INSERT (USERNAME, SESSION_ID, DEVICE_NAME, LOGIN_TIME)
        VALUES (p_username, p_session, p_device, SYSTIMESTAMP);
END;
/
CREATE OR REPLACE FUNCTION CHECK_SESSION (
    p_username IN VARCHAR2,
    p_session  IN VARCHAR2
) RETURN NUMBER
AS
    v_sess VARCHAR2(100);
BEGIN
    SELECT SESSION_ID INTO v_sess
    FROM APP_SESSION
    WHERE USERNAME = p_username;

    IF v_sess = p_session THEN
        RETURN 1;  -- session h?p l?
    ELSE
        RETURN 0;  -- b? máy khác login
    END IF;

EXCEPTION WHEN NO_DATA_FOUND THEN
    RETURN 0;
END;
/

CREATE OR REPLACE PACKAGE ADMIN_USER_MGR AS
    PROCEDURE GRANT_PRIV(
        p_user      IN VARCHAR2,
        p_table     IN VARCHAR2,
        p_privs     IN VARCHAR2 -- 'SELECT,INSERT,UPDATE'
    );

    PROCEDURE REVOKE_PRIV(
        p_user      IN VARCHAR2,
        p_table     IN VARCHAR2,
        p_privs     IN VARCHAR2
    );

    PROCEDURE LIST_PRIVS(
        p_user IN VARCHAR2,
        p_cur  OUT SYS_REFCURSOR
    );
END ADMIN_USER_MGR;
/
CREATE OR REPLACE PACKAGE BODY ADMIN_USER_MGR AS

    PROCEDURE GRANT_PRIV(
        p_user  IN VARCHAR2,
        p_table IN VARCHAR2,
        p_privs IN VARCHAR2
    ) IS
        v_sql VARCHAR2(4000);
    BEGIN
        v_sql := 'GRANT ' || p_privs || ' ON ' || p_table || ' TO ' || p_user;
        EXECUTE IMMEDIATE v_sql;
    END;

    PROCEDURE REVOKE_PRIV(
        p_user  IN VARCHAR2,
        p_table IN VARCHAR2,
        p_privs IN VARCHAR2
    ) IS
        v_sql VARCHAR2(4000);
    BEGIN
        v_sql := 'REVOKE ' || p_privs || ' ON ' || p_table || ' FROM ' || p_user;
        EXECUTE IMMEDIATE v_sql;
    END;

    PROCEDURE LIST_PRIVS(
        p_user IN VARCHAR2,
        p_cur  OUT SYS_REFCURSOR
    ) IS
    BEGIN
        OPEN p_cur FOR
            SELECT TABLE_NAME, PRIVILEGE
            FROM DBA_TAB_PRIVS
            WHERE GRANTEE = UPPER(p_user)
            ORDER BY TABLE_NAME;
    END;

END ADMIN_USER_MGR;
/
GRANT EXECUTE ON ADMIN_USER_MGR TO QLVT_USER;
GRANT SELECT ON DBA_TAB_PRIVS TO QLVT_USER;


CREATE TABLE AUDIT_USER_LOG (
    ID          NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    ACTION      VARCHAR2(50),
    TARGET_USER VARCHAR2(50),
    ACTION_BY   VARCHAR2(50),
    ACTION_TIME TIMESTAMP DEFAULT SYSTIMESTAMP,
    HOST        VARCHAR2(100),
    IP_ADDRESS  VARCHAR2(50),
    DETAIL      VARCHAR2(4000)
);
CREATE OR REPLACE PROCEDURE LOG_USER_ACTION (
    p_action      VARCHAR2,
    p_target_user VARCHAR2,
    p_detail      VARCHAR2
) AS
BEGIN
    INSERT INTO AUDIT_USER_LOG (
        ACTION, TARGET_USER, ACTION_BY, HOST, IP_ADDRESS, DETAIL
    )
    VALUES (
        p_action,
        p_target_user,
        SYS_CONTEXT('USERENV', 'SESSION_USER'),
        SYS_CONTEXT('USERENV', 'HOST'),
        SYS_CONTEXT('USERENV', 'IP_ADDRESS'),
        p_detail
    );
END;
/
PROCEDURE LOCK_USER(p_user VARCHAR2) IS
BEGIN
    EXECUTE IMMEDIATE 
        'ALTER USER ' || DBMS_ASSERT.simple_sql_name(p_user) || ' ACCOUNT LOCK';

    LOG_USER_ACTION('LOCK', p_user, 'Khóa tài kho?n');
END;

CREATE TABLE USER_AUDIT_LOG (
    ID          NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    ACTION_TYPE VARCHAR2(30),     -- LOCK, UNLOCK, DROP, CREATE, GRANT
    TARGET_USER VARCHAR2(30),     -- user b? tác ð?ng
    ACTION_BY   VARCHAR2(30),     -- user ðang ðãng nh?p
    ACTION_TIME TIMESTAMP DEFAULT SYSTIMESTAMP,
    DETAIL      VARCHAR2(4000)
);
CREATE OR REPLACE PROCEDURE LOG_USER_ACTION (
    p_action   IN VARCHAR2,
    p_target   IN VARCHAR2,
    p_detail   IN VARCHAR2
)
AS
BEGIN
    INSERT INTO USER_AUDIT_LOG
    (ACTION_TYPE, TARGET_USER, ACTION_BY, DETAIL)
    VALUES
    (p_action, p_target, USER, p_detail);

    COMMIT;
END;
/
CREATE OR REPLACE PROCEDURE LOCK_USER (
    p_user IN VARCHAR2
)
AS
BEGIN
    EXECUTE IMMEDIATE
        'ALTER USER ' || DBMS_ASSERT.simple_sql_name(p_user) || ' ACCOUNT LOCK';

    LOG_USER_ACTION('LOCK', p_user, 'Khóa tài kho?n');
END;
/
CREATE OR REPLACE PROCEDURE UNLOCK_USER (
    p_user IN VARCHAR2
)
AS
BEGIN
    EXECUTE IMMEDIATE
        'ALTER USER ' || DBMS_ASSERT.simple_sql_name(p_user) || ' ACCOUNT UNLOCK';

    LOG_USER_ACTION('UNLOCK', p_user, 'M? khóa tài kho?n');
END;
/
CREATE OR REPLACE PROCEDURE DROP_USER_APP (
    p_user IN VARCHAR2
)
AS
BEGIN
    EXECUTE IMMEDIATE
        'DROP USER ' || DBMS_ASSERT.simple_sql_name(p_user) || ' CASCADE';

    LOG_USER_ACTION('DROP', p_user, 'Xóa user kh?i h? th?ng');
END;
/
CREATE OR REPLACE PROCEDURE GRANT_TABLE_PRIV (
    p_user  IN VARCHAR2,
    p_table IN VARCHAR2,
    p_priv  IN VARCHAR2
)
AS
BEGIN
    EXECUTE IMMEDIATE
        'GRANT ' || p_priv || ' ON ' ||
        DBMS_ASSERT.simple_sql_name(p_table) ||
        ' TO ' || DBMS_ASSERT.simple_sql_name(p_user);

    LOG_USER_ACTION('GRANT', p_user,
        'Grant ' || p_priv || ' on ' || p_table);
END;
/
CREATE OR REPLACE PROCEDURE REVOKE_TABLE_PRIV (
    p_user  IN VARCHAR2,
    p_table IN VARCHAR2,
    p_priv  IN VARCHAR2
)
AS
BEGIN
    EXECUTE IMMEDIATE
        'REVOKE ' || p_priv || ' ON ' ||
        DBMS_ASSERT.simple_sql_name(p_table) ||
        ' FROM ' || DBMS_ASSERT.simple_sql_name(p_user);

    LOG_USER_ACTION('REVOKE', p_user,
        'Revoke ' || p_priv || ' on ' || p_table);
END;
/

SELECT ACTION_TYPE,
       TARGET_USER,
       ACTION_BY,
       ACTION_TIME,
       DETAIL
FROM USER_AUDIT_LOG
ORDER BY ACTION_TIME DESC


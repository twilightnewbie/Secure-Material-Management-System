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
SELECT USERNAME FROM USER_USERS;
SELECT USERNAME
FROM ALL_USERS
ORDER BY USERNAME;

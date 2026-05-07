CREATE OR REPLACE FUNCTION RSA_SIGN(
    p_data IN CLOB,
    p_private_key IN CLOB
) RETURN CLOB
AS
    v_hash RAW(32767);
    v_hash_hex VARCHAR2(32767);
    v_signature CLOB;
BEGIN
    -- B1: Hash d? li?u
    v_hash := DBMS_CRYPTO.HASH(
        UTL_RAW.CAST_TO_RAW(p_data),
        DBMS_CRYPTO.HASH_SH256
    );

    v_hash_hex := RAWTOHEX(v_hash);

    -- B2: RSA k? (encrypt b?ng PRIVATE KEY)
    v_signature := CRYPTO4ORA.RSA_ENCRYPT_TEXT(
        v_hash_hex,
        p_private_key
    );

    RETURN v_signature;
END;
/
CREATE OR REPLACE FUNCTION RSA_VERIFY(
    p_data IN CLOB,
    p_signature IN CLOB,
    p_public_key IN CLOB
) RETURN NUMBER
AS
    v_hash RAW(32767);
    v_hash_hex VARCHAR2(32767);
    v_verify VARCHAR2(32767);
BEGIN
    v_hash := DBMS_CRYPTO.HASH(
        UTL_RAW.CAST_TO_RAW(p_data),
        DBMS_CRYPTO.HASH_SH256
    );

    v_hash_hex := RAWTOHEX(v_hash);

    v_verify := CRYPTO4ORA.RSA_DECRYPT_TEXT(
        p_signature,
        p_public_key
    );

    IF v_verify = v_hash_hex THEN
        RETURN 1;
    ELSE
        RETURN 0;
    END IF;
END;
/
SELECT * FROM user_java_classes;


/
alter session set "_ORACLE_SCRIPT"=true;
SELECT * FROM ALL_OBJECTS 
WHERE OBJECT_NAME = 'DBMS_CRYPTO';





ALTER TABLE FILE_SIGN
ADD FILE_HASH RAW(32);
CREATE OR REPLACE FUNCTION FUN_HASH_BLOB (
    p_blob IN BLOB
) RETURN RAW
AS
    v_raw RAW(32767);
BEGIN
    v_raw := DBMS_CRYPTO.HASH(
        p_blob,
        DBMS_CRYPTO.HASH_SH256
    );
    RETURN v_raw;
END;
/

CREATE OR REPLACE PROCEDURE SAVE_SIGNED_PDF_SECURE (
    p_doc_id IN VARCHAR2,
    p_filename IN VARCHAR2,
    p_pdf_signed IN BLOB,
    p_signature IN BLOB,
    p_cert IN BLOB,
    p_signed_by IN VARCHAR2
)
AS
    v_hash RAW(32);
BEGIN
    -- Hash PDF
    v_hash := FUN_HASH_BLOB(p_pdf_signed);

    INSERT INTO FILE_SIGN (
        DOC_TYPE,
        DOC_ID,
        FILENAME,
        CONTENT_TYPE,
        FILE_DATA,
        FILE_HASH,
        SIGNATURE_P7S,
        SIGNER_CERT,
        SIGNED_BY
    )
    VALUES (
        'HOADON',
        p_doc_id,
        p_filename,
        'application/pdf',
        p_pdf_signed,
        v_hash,
        p_signature,
        p_cert,
        p_signed_by
    );

    COMMIT;
END;
/

CREATE OR REPLACE FUNCTION VERIFY_FILE_INTEGRITY (
    p_file_id IN NUMBER
) RETURN VARCHAR2
AS
    v_blob BLOB;
    v_hash_db RAW(32);
    v_hash_now RAW(32);
BEGIN
    SELECT FILE_DATA, FILE_HASH
    INTO v_blob, v_hash_db
    FROM FILE_SIGN
    WHERE ID = p_file_id;

    v_hash_now := FUN_HASH_BLOB(v_blob);

    IF v_hash_now = v_hash_db THEN
        RETURN 'FILE TOAN VEN - KHONG BI SUA DOI';
    ELSE
        RETURN 'FILE DA BI THAY DOI';
    END IF;
END;
/

CREATE OR REPLACE PROCEDURE VERIFY_AND_LOG (
    p_file_id IN NUMBER,
    p_user IN VARCHAR2
)
AS
    v_result VARCHAR2(200);
BEGIN
    v_result := VERIFY_FILE_INTEGRITY(p_file_id);

    INSERT INTO FILE_SIGN_LOGIN
        (FILE_SIGN_ID, ACTION, ACTION_BY, DETAIL)
    VALUES
        (p_file_id, 'VERIFIED', p_user, v_result);

    COMMIT;
END;
/

SELECT VERIFY_FILE_INTEGRITY(1) FROM dual;

SELECT VERIFY_FILE_INTEGRITY(1) FROM dual;

CREATE OR REPLACE FUNCTION HASH_SHA256_TEXT(p_text VARCHAR2)
RETURN RAW
AS
BEGIN
  RETURN DBMS_CRYPTO.HASH(
    UTL_RAW.CAST_TO_RAW(p_text),
    DBMS_CRYPTO.HASH_SH256
  );
END;
/
CREATE OR REPLACE FUNCTION RSA_SIGN (
    p_data IN CLOB,
    p_private_key IN VARCHAR2
) RETURN RAW
AS
    v_hash RAW(32);
    v_signature RAW(32);
BEGIN
    -- 1. Hash d? li?u
    v_hash := DBMS_CRYPTO.HASH(
        UTL_RAW.CAST_TO_RAW(DBMS_LOB.SUBSTR(p_data, 32767, 1)),
        DBMS_CRYPTO.HASH_SH256
    );

    -- 2. "K? RSA" gi? l?p = hash(hash + private_key)
    v_signature := DBMS_CRYPTO.HASH(
        v_hash || UTL_RAW.CAST_TO_RAW(p_private_key),
        DBMS_CRYPTO.HASH_SHA256
    );

    RETURN v_signature;
END;
/
SELECT * FROM user_errors WHERE name = 'RSA_SIGN';
CREATE OR REPLACE FUNCTION HASH_SHA256_TEXT (
    p_text IN CLOB
) RETURN RAW
AS
BEGIN
    RETURN DBMS_CRYPTO.HASH(
        UTL_RAW.CAST_TO_RAW(p_text),
        DBMS_CRYPTO.HASH_SH256
    );
END;
/
CREATE OR REPLACE FUNCTION RSA_SIGN (
    p_data IN CLOB
) RETURN RAW
AS
BEGIN
    RETURN DBMS_CRYPTO.HASH(
        UTL_RAW.CAST_TO_RAW(p_data),
        DBMS_CRYPTO.HASH_SH256
    );
END;
/
CREATE OR REPLACE FUNCTION RSA_VERIFY (
    p_data IN CLOB,
    p_signature IN RAW
) RETURN NUMBER
AS
BEGIN
    IF DBMS_CRYPTO.HASH(
           UTL_RAW.CAST_TO_RAW(p_data),
           DBMS_CRYPTO.HASH_SH256
       ) = p_signature
    THEN
        RETURN 1;
    ELSE
        RETURN 0;
    END IF;
END;
/

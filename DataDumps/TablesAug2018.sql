--
-- File generated with SQLiteStudio v3.1.1 on Wed Aug 22 20:19:35 2018
--
-- Text encoding used: System
--
PRAGMA foreign_keys = off;
BEGIN TRANSACTION;

-- Table: Achievement
CREATE TABLE Achievement (
    Achievement_ID INTEGER    PRIMARY KEY
                              NOT NULL
                              UNIQUE,
    Level          INTEGER    NOT NULL,
    Hint           TEXT,
    BaseObjectID   INTEGER NOT NULL,
    FOREIGN KEY (
        BaseObjectID
    )
    REFERENCES BaseObject(BaseObject_ID) ON UPDATE CASCADE
                          ON DELETE CASCADE DEFERRABLE INITIALLY DEFERRED
);

-- Table: Animation
CREATE TABLE Animation (
    Animation_ID       INTEGER  PRIMARY KEY
                                NOT NULL
                                UNIQUE,
    StateAnimationType SMALLINT,
    Frames             INTEGER  NOT NULL,
    FrameRate          INTEGER  NOT NULL
                                DEFAULT 60,
    ImageSet           BLOB,
    BaseObjectID       INTEGER  NOT NULL,
    FOREIGN KEY (BaseObjectID) REFERENCES BaseObject(BaseObject_ID) ON UPDATE CASCADE ON DELETE CASCADE
);

-- Table: BaseObject
CREATE TABLE BaseObject (
    BaseObject_ID INTEGER   PRIMARY KEY
                            NOT NULL
                            UNIQUE,
    Name          CHAR (20) NOT NULL,
    Description   TEXT,
    Image         BLOB,
    Created       DATETIME  NOT NULL
                            DEFAULT (DateTime('now', 'localtime') ),
    Updated       DATETIME  NOT NULL
                            DEFAULT (DateTime('now', 'localtime') ) 
);
INSERT INTO BaseObject (BaseObject_ID, Name, Description, Image, Created, Updated) VALUES (1, 'Fighter', '', NULL, '2017-12-01 17:01:16', '2018-08-22 15:57:22');
INSERT INTO BaseObject (BaseObject_ID, Name, Description, Image, Created, Updated) VALUES (2, 'Explorer', '', NULL, '2017-12-01 17:01:16', '2018-08-22 15:57:22');
INSERT INTO BaseObject (BaseObject_ID, Name, Description, Image, Created, Updated) VALUES (3, 'Warrior', '', NULL, '2017-12-01 17:01:16', '2018-08-22 15:57:22');
INSERT INTO BaseObject (BaseObject_ID, Name, Description, Image, Created, Updated) VALUES (4, 'Guard Brawler', '', NULL, '2017-12-01 17:01:16', '2018-08-22 15:57:22');
INSERT INTO BaseObject (BaseObject_ID, Name, Description, Image, Created, Updated) VALUES (5, 'Berserker', '', NULL, '2017-12-01 17:01:16', '2018-08-22 15:57:22');
INSERT INTO BaseObject (BaseObject_ID, Name, Description, Image, Created, Updated) VALUES (6, 'Thief', '', NULL, '2017-12-01 17:01:16', '2018-08-22 15:57:22');
INSERT INTO BaseObject (BaseObject_ID, Name, Description, Image, Created, Updated) VALUES (7, 'Assassin', '', NULL, '2017-12-01 17:01:16', '2018-08-22 15:57:22');
INSERT INTO BaseObject (BaseObject_ID, Name, Description, Image, Created, Updated) VALUES (8, 'Mage', '', NULL, '2017-12-01 17:01:16', '2018-08-22 15:57:22');
INSERT INTO BaseObject (BaseObject_ID, Name, Description, Image, Created, Updated) VALUES (9, 'Elementalist', '', NULL, '2017-12-01 17:01:16', '2018-08-22 15:57:22');
INSERT INTO BaseObject (BaseObject_ID, Name, Description, Image, Created, Updated) VALUES (10, 'Reaper', '', NULL, '2017-12-01 17:01:16', '2018-08-22 15:57:22');
INSERT INTO BaseObject (BaseObject_ID, Name, Description, Image, Created, Updated) VALUES (11, 'Paladin', '', NULL, '2017-12-01 17:01:16', '2018-08-22 15:57:22');
INSERT INTO BaseObject (BaseObject_ID, Name, Description, Image, Created, Updated) VALUES (12, 'Grand Knight', '', NULL, '2017-12-01 17:01:16', '2018-08-22 15:57:22');
INSERT INTO BaseObject (BaseObject_ID, Name, Description, Image, Created, Updated) VALUES (13, 'Cleric', '', NULL, '2017-12-01 17:01:16', '2018-08-22 15:57:22');
INSERT INTO BaseObject (BaseObject_ID, Name, Description, Image, Created, Updated) VALUES (14, 'Lune Sage', '', NULL, '2017-12-01 17:01:16', '2018-08-22 15:57:22');
INSERT INTO BaseObject (BaseObject_ID, Name, Description, Image, Created, Updated) VALUES (15, 'Floromancer', '', NULL, '2017-12-01 17:01:16', '2018-08-22 15:57:22');
INSERT INTO BaseObject (BaseObject_ID, Name, Description, Image, Created, Updated) VALUES (16, 'Mechanic', '', NULL, '2017-12-01 17:01:16', '2018-08-22 15:57:22');
INSERT INTO BaseObject (BaseObject_ID, Name, Description, Image, Created, Updated) VALUES (17, 'Gadgetmaster', '', NULL, '2017-12-01 17:01:16', '2018-08-22 15:57:22');
INSERT INTO BaseObject (BaseObject_ID, Name, Description, Image, Created, Updated) VALUES (18, 'Gunner', '', NULL, '2017-12-01 17:01:16', '2018-08-22 15:57:22');
INSERT INTO BaseObject (BaseObject_ID, Name, Description, Image, Created, Updated) VALUES (19, 'Ranger', '', NULL, '2017-12-01 17:01:16', '2018-08-22 15:57:22');
INSERT INTO BaseObject (BaseObject_ID, Name, Description, Image, Created, Updated) VALUES (20, 'Spy', '', NULL, '2017-12-01 17:01:16', '2018-08-22 15:57:22');

-- Table: BattlerClass
CREATE TABLE BattlerClass (
    BattlerClass_ID   INTEGER PRIMARY KEY
                              NOT NULL
                              UNIQUE,
    UpgradedClass1    INTEGER,
    UpgradedClass2    INTEGER,
    UsableWeaponType1 INTEGER,
    UsableWeaponType2 INTEGER,
    BaseObjectID      INTEGER NOT NULL,
    ScaledStats       INTEGER NOT NULL,
    FOREIGN KEY (
        BaseObjectID
    )
    REFERENCES BaseObject (BaseObject_ID) ON UPDATE CASCADE
                                          ON DELETE CASCADE DEFERRABLE INITIALLY DEFERRED,
    FOREIGN KEY (
        ScaledStats
    )
    REFERENCES Stats (Stats_ID) ON DELETE CASCADE DEFERRABLE INITIALLY DEFERRED
);
INSERT INTO BattlerClass (BattlerClass_ID, UpgradedClass1, UpgradedClass2, UsableWeaponType1, UsableWeaponType2, BaseObjectID, ScaledStats) VALUES (1, 2, 5, 1, NULL, 1, 1);
INSERT INTO BattlerClass (BattlerClass_ID, UpgradedClass1, UpgradedClass2, UsableWeaponType1, UsableWeaponType2, BaseObjectID, ScaledStats) VALUES (2, NULL, NULL, 1, 3, 2, 2);
INSERT INTO BattlerClass (BattlerClass_ID, UpgradedClass1, UpgradedClass2, UsableWeaponType1, UsableWeaponType2, BaseObjectID, ScaledStats) VALUES (3, 4, 5, 2, NULL, 3, 3);
INSERT INTO BattlerClass (BattlerClass_ID, UpgradedClass1, UpgradedClass2, UsableWeaponType1, UsableWeaponType2, BaseObjectID, ScaledStats) VALUES (4, NULL, NULL, 2, NULL, 4, 4);
INSERT INTO BattlerClass (BattlerClass_ID, UpgradedClass1, UpgradedClass2, UsableWeaponType1, UsableWeaponType2, BaseObjectID, ScaledStats) VALUES (5, NULL, NULL, 2, 4, 5, 5);
INSERT INTO BattlerClass (BattlerClass_ID, UpgradedClass1, UpgradedClass2, UsableWeaponType1, UsableWeaponType2, BaseObjectID, ScaledStats) VALUES (6, 7, 10, 1, NULL, 6, 6);
INSERT INTO BattlerClass (BattlerClass_ID, UpgradedClass1, UpgradedClass2, UsableWeaponType1, UsableWeaponType2, BaseObjectID, ScaledStats) VALUES (7, NULL, NULL, 1, 4, 7, 7);
INSERT INTO BattlerClass (BattlerClass_ID, UpgradedClass1, UpgradedClass2, UsableWeaponType1, UsableWeaponType2, BaseObjectID, ScaledStats) VALUES (8, 9, 10, 3, NULL, 8, 8);
INSERT INTO BattlerClass (BattlerClass_ID, UpgradedClass1, UpgradedClass2, UsableWeaponType1, UsableWeaponType2, BaseObjectID, ScaledStats) VALUES (9, NULL, NULL, 3, NULL, 9, 9);
INSERT INTO BattlerClass (BattlerClass_ID, UpgradedClass1, UpgradedClass2, UsableWeaponType1, UsableWeaponType2, BaseObjectID, ScaledStats) VALUES (10, NULL, NULL, 1, 4, 10, 10);
INSERT INTO BattlerClass (BattlerClass_ID, UpgradedClass1, UpgradedClass2, UsableWeaponType1, UsableWeaponType2, BaseObjectID, ScaledStats) VALUES (11, 12, 15, 1, NULL, 11, 11);
INSERT INTO BattlerClass (BattlerClass_ID, UpgradedClass1, UpgradedClass2, UsableWeaponType1, UsableWeaponType2, BaseObjectID, ScaledStats) VALUES (12, NULL, NULL, 1, 2, 12, 12);
INSERT INTO BattlerClass (BattlerClass_ID, UpgradedClass1, UpgradedClass2, UsableWeaponType1, UsableWeaponType2, BaseObjectID, ScaledStats) VALUES (13, 14, 15, 3, NULL, 13, 13);
INSERT INTO BattlerClass (BattlerClass_ID, UpgradedClass1, UpgradedClass2, UsableWeaponType1, UsableWeaponType2, BaseObjectID, ScaledStats) VALUES (14, NULL, NULL, 3, NULL, 14, 14);
INSERT INTO BattlerClass (BattlerClass_ID, UpgradedClass1, UpgradedClass2, UsableWeaponType1, UsableWeaponType2, BaseObjectID, ScaledStats) VALUES (15, NULL, NULL, 2, 3, 15, 15);
INSERT INTO BattlerClass (BattlerClass_ID, UpgradedClass1, UpgradedClass2, UsableWeaponType1, UsableWeaponType2, BaseObjectID, ScaledStats) VALUES (16, 17, 20, 5, NULL, 16, 16);
INSERT INTO BattlerClass (BattlerClass_ID, UpgradedClass1, UpgradedClass2, UsableWeaponType1, UsableWeaponType2, BaseObjectID, ScaledStats) VALUES (17, NULL, NULL, 5, NULL, 17, 17);
INSERT INTO BattlerClass (BattlerClass_ID, UpgradedClass1, UpgradedClass2, UsableWeaponType1, UsableWeaponType2, BaseObjectID, ScaledStats) VALUES (18, 19, 20, 0, NULL, 18, 18);
INSERT INTO BattlerClass (BattlerClass_ID, UpgradedClass1, UpgradedClass2, UsableWeaponType1, UsableWeaponType2, BaseObjectID, ScaledStats) VALUES (19, NULL, NULL, 3, 4, 19, 19);
INSERT INTO BattlerClass (BattlerClass_ID, UpgradedClass1, UpgradedClass2, UsableWeaponType1, UsableWeaponType2, BaseObjectID, ScaledStats) VALUES (20, NULL, NULL, 4, 5, 20, 20);

-- Table: Command
CREATE TABLE Command (
    Command_ID    INTEGER  PRIMARY KEY
                           NOT NULL
                           UNIQUE,
    RapidTapBoost DOUBLE   NOT NULL,
    KeyInput      CHAR (1) NOT NULL,
    StartFrame    INTEGER  NOT NULL,
    EndFrame      INTEGER  NOT NULL,
    Size          DOUBLE   NOT NULL,
    LocationX1    INTEGER  NOT NULL,
    LocationX2    INTEGER  NOT NULL,
    LocationY1    INTEGER  NOT NULL,
    LocationY2    INTEGER  NOT NULL,
    Range         DOUBLE   NOT NULL,
    AnimationID   INTEGER  NOT NULL,
    FOREIGN KEY (
        AnimationID
    )
    REFERENCES Animation(Animation_ID) ON DELETE CASCADE
);

-- Table: EGSingleEnemy
CREATE TABLE EGSingleEnemy (EGSingleEnemies_ID INTEGER PRIMARY KEY NOT NULL UNIQUE, EnemyID INTEGER NOT NULL, EnemyGroupID INTEGER NOT NULL, FOREIGN KEY (EnemyID) REFERENCES Enemy (Enemy_ID) ON DELETE CASCADE, FOREIGN KEY (EnemyGroupID) REFERENCES EnemyGroup (EnemyGroup_ID) ON DELETE CASCADE);

-- Table: Enemy
CREATE TABLE Enemy (Enemy_ID INTEGER PRIMARY KEY NOT NULL UNIQUE, BossType INTEGER NOT NULL DEFAULT 0, Exp INTEGER NOT NULL, Gold INTEGER NOT NULL, BaseObjectID INTEGER NOT NULL REFERENCES BaseObject (BaseObject_ID) ON DELETE CASCADE ON UPDATE CASCADE DEFERRABLE INITIALLY DEFERRED, ScaledStats INTEGER NOT NULL, FOREIGN KEY (BaseObjectID) REFERENCES BaseObject (BaseObject_ID) ON DELETE CASCADE ON UPDATE CASCADE DEFERRABLE INITIALLY DEFERRED, FOREIGN KEY (ScaledStats) REFERENCES Stats (Stats_ID) ON UPDATE CASCADE);

-- Table: EnemyGroup
CREATE TABLE EnemyGroup (EnemyGroup_ID INTEGER PRIMARY KEY NOT NULL UNIQUE, Floor BLOB, BaseObjectID INTEGER NOT NULL, FOREIGN KEY (BaseObjectID) REFERENCES BaseObject (BaseObject_ID) ON UPDATE CASCADE ON DELETE CASCADE DEFERRABLE INITIALLY DEFERRED);

-- Table: Environment
CREATE TABLE Environment (
    Environment_ID  INTEGER PRIMARY KEY
                            NOT NULL
                            UNIQUE,
    TemperatureType INTEGER NOT NULL,
    Background      BLOB,
    Foreground      BLOB,
    BaseObjectID    INTEGER NOT NULL,
    FOREIGN KEY (
        BaseObjectID
    )
    REFERENCES BaseObject(BaseObject_ID) ON UPDATE CASCADE
                          ON DELETE CASCADE DEFERRABLE INITIALLY DEFERRED
);

-- Table: Event
CREATE TABLE Event (
    Event_ID           INTEGER PRIMARY KEY
                               NOT NULL
                               UNIQUE,
    TriggerInteraction INTEGER NOT NULL
                               DEFAULT 0,
    Speed              DOUBLE  NOT NULL
                               DEFAULT 0,
    MoveFrequency              NOT NULL
                               DEFAULT 0,
    BaseObjectID       INTEGER NOT NULL,
    EnemyGroupID       INTEGER,
    MapID              INTEGER,
    FOREIGN KEY (
        BaseObjectID
    )
    REFERENCES BaseObject(BaseObject_ID) ON UPDATE CASCADE
                           ON DELETE CASCADE DEFERRABLE INITIALLY DEFERRED,
    FOREIGN KEY (
        EnemyGroupID
    )
    REFERENCES EnemyGroup (EnemyGroup_ID),
    FOREIGN KEY (
        MapID
    )
    REFERENCES Map(Map_ID) ON DELETE CASCADE
);

-- Table: Item
CREATE TABLE Item (
    Item_ID         INTEGER PRIMARY KEY
                            NOT NULL
                            UNIQUE,
    DefaultQuantity INTEGER NOT NULL
                            DEFAULT 3,
    DefaultPrice    INTEGER NOT NULL,
    Consumable      BOOLEAN NOT NULL
                            DEFAULT TRUE,
    ToolID          INTEGER NOT NULL,
    WeaponID        INTEGER,
    StatMods        INTEGER,
    BaseObjectID    INTEGER REFERENCES BaseObject (BaseObject_ID) ON DELETE CASCADE
                                                                  ON UPDATE CASCADE DEFERRABLE INITIALLY DEFERRED,
    FOREIGN KEY (
        ToolID
    )
    REFERENCES Tool ON UPDATE CASCADE
                    ON DELETE CASCADE DEFERRABLE INITIALLY DEFERRED,
    FOREIGN KEY (
        WeaponID
    )
    REFERENCES Weapon (Weapon_ID) ON UPDATE CASCADE
                                  ON DELETE CASCADE DEFERRABLE INITIALLY DEFERRED,
    FOREIGN KEY (
        StatMods
    )
    REFERENCES Stats (Stats_ID) ON DELETE CASCADE DEFERRABLE INITIALLY DEFERRED
);

-- Table: Map
CREATE TABLE Map (
    Map_ID        INTEGER PRIMARY KEY
                          NOT NULL
                          UNIQUE,
    Width         INTEGER NOT NULL,
    Height        INTEGER NOT NULL,
    BaseObjectID  INTEGER,
    EnvironmentID INTEGER,
    FOREIGN KEY (
        BaseObjectID
    )
    REFERENCES BaseObject(BaseObject_ID) ON UPDATE CASCADE
                          ON DELETE CASCADE DEFERRABLE INITIALLY DEFERRED,
    FOREIGN KEY (
        EnvironmentID
    )
    REFERENCES Environment (Environment_ID) 
);

-- Table: PassiveSkill
CREATE TABLE PassiveSkill (
    PassiveSkill_ID INTEGER PRIMARY KEY
                            NOT NULL
                            UNIQUE,
    Scope           INTEGER NOT NULL
                            DEFAULT 0,
    HPMin           INTEGER NOT NULL
                            DEFAULT 0,
    HPMax           INTEGER NOT NULL
                            DEFAULT 100,
    SPMin           INTEGER NOT NULL
                            DEFAULT 0,
    SPMax           INTEGER NOT NULL
                            DEFAULT 100,
    AnyState        BOOLEAN NOT NULL
                            DEFAULT FALSE,
    NoState         BOOLEAN NOT NULL
                            DEFAULT FALSE,
    StateActive1    INTEGER,
    StateActive2    INTEGER,
    StateInactive1  INTEGER,
    StateInactive2  INTEGER,
    ExtraExpGain    INTEGER NOT NULL
                            DEFAULT 0,
    ExtraHPGain     INTEGER NOT NULL
                            DEFAULT 0,
    BaseObjectID    INTEGER NOT NULL,
    EventID INTEGER,
    FOREIGN KEY (BaseObjectID) REFERENCES BaseObject(BaseObject_ID) ON DELETE CASCADE ON UPDATE CASCADE DEFERRABLE INITIALLY DEFERRED
    FOREIGN KEY (
        EventID
    )
    REFERENCES Event (Event_ID) ON UPDATE CASCADE
);

-- Table: Platform
CREATE TABLE Platform (
    Platform_ID    INTEGER PRIMARY KEY
                           NOT NULL
                           UNIQUE,
    FloorDamage    INTEGER NOT NULL
                           DEFAULT 0,
    BounceVelocity DOUBLE  NOT NULL
                           DEFAULT 0,
    LandingDamp    DOUBLE  NOT NULL
                           DEFAULT 1,
    BaseObjectID   INTEGER NOT NULL,
    FOREIGN KEY (
        BaseObjectID
    )
    REFERENCES BaseObject(BaseObject_ID) ON UPDATE CASCADE
                          ON DELETE CASCADE DEFERRABLE INITIALLY DEFERRED
);

-- Table: Player
CREATE TABLE Player (
    Player_ID         INTEGER PRIMARY KEY
                              NOT NULL
                              UNIQUE,
    ElementRates      TEXT,
    Companionship     INTEGER NOT NULL
                              DEFAULT (100),
    SavePartnerRate   INTEGER NOT NULL
                              DEFAULT (100),
    CounterattackRate INTEGER NOT NULL
                              DEFAULT (100),
    AssistDamageRate  INTEGER NOT NULL
                              DEFAULT (100),
    BaseObjectID      INTEGER NOT NULL,
    NaturalStats      INTEGER NOT NULL,
    FOREIGN KEY (
        BaseObjectID
    )
    REFERENCES BaseObject (BaseObject_ID) ON UPDATE CASCADE
                                          ON DELETE CASCADE DEFERRABLE INITIALLY DEFERRED,
    FOREIGN KEY (
        NaturalStats
    )
    REFERENCES Stats (Stats_ID) 
);

-- Table: Player_To_BattlerClass
CREATE TABLE Player_To_BattlerClass(
    PlayerID INTEGER NOT NULL,
    BattlerClassID INTEGER NOT NULL,
    TableIndex INTEGER NOT NULL,
    PRIMARY KEY (PlayerID, BattlerClassID),
    FOREIGN KEY (PlayerID) REFERENCES Player(Player_ID) ON DELETE CASCADE ON UPDATE CASCADE DEFERRABLE INITIALLY DEFERRED
    FOREIGN KEY (BattlerClassID) REFERENCES BattlerClass(BattlerClass_ID) ON DELETE CASCADE ON UPDATE CASCADE DEFERRABLE INITIALLY DEFERRED
);

-- Table: Player_To_Player
CREATE TABLE Player_To_Player(
    PlayerID INTEGER NOT NULL,
    OtherPlayerID INTEGER NOT NULL,
    TableIndex INTEGER NOT NULL,
    CompanionshipTo INTEGER NOT NULL,
    PRIMARY KEY (PlayerID, OtherPlayerID),
    FOREIGN KEY (PlayerID) REFERENCES Player(Player_ID) ON DELETE CASCADE ON UPDATE CASCADE DEFERRABLE INITIALLY DEFERRED
    FOREIGN KEY (OtherPlayerID) REFERENCES Player(Player_ID) ON DELETE CASCADE ON UPDATE CASCADE DEFERRABLE INITIALLY DEFERRED
);

-- Table: Player_To_Skill
CREATE TABLE Player_To_Skill(
    PlayerID INTEGER NOT NULL,
    SkillID INTEGER NOT NULL,
    TableIndex INTEGER NOT NULL,
    LevelRequired INTEGER NOT NULL,
    PRIMARY KEY (PlayerID, SkillID),
    FOREIGN KEY (PlayerID) REFERENCES Player(Player_ID) ON DELETE CASCADE ON UPDATE CASCADE DEFERRABLE INITIALLY DEFERRED
    FOREIGN KEY (SkillID) REFERENCES Skill(Skill_ID) ON DELETE CASCADE ON UPDATE CASCADE DEFERRABLE INITIALLY DEFERRED
);

-- Table: Player_To_State
CREATE TABLE Player_To_State(
    PlayerID INTEGER NOT NULL,
    StateID INTEGER NOT NULL,
    TableIndex INTEGER NOT NULL,
    Vulnerability INTEGER NOT NULL,
    PRIMARY KEY (PlayerID, StateID),
    FOREIGN KEY (PlayerID) REFERENCES Player(Player_ID) ON DELETE CASCADE ON UPDATE CASCADE DEFERRABLE INITIALLY DEFERRED
    FOREIGN KEY (StateID) REFERENCES State(State_ID) ON DELETE CASCADE ON UPDATE CASCADE DEFERRABLE INITIALLY DEFERRED
);

-- Table: Projectile
CREATE TABLE Projectile (
    Projectile_ID INTEGER PRIMARY KEY
                          NOT NULL
                          UNIQUE,
    BaseObjectID  INTEGER NOT NULL,
    FOREIGN KEY (
        BaseObjectID
    )
    REFERENCES BaseObject(BaseObject_ID) ON UPDATE CASCADE
                          ON DELETE CASCADE DEFERRABLE INITIALLY DEFERRED
);

-- Table: Skill
CREATE TABLE Skill (
    Skill_ID      INTEGER PRIMARY KEY
                          NOT NULL
                          UNIQUE,
    SPConsume     INTEGER NOT NULL
                          DEFAULT (0),
    NumberOfUsers INTEGER NOT NULL
                          DEFAULT 1,
    Charge        INTEGER NOT NULL
                          DEFAULT 0,
    WarmUp        INTEGER NOT NULL
                          DEFAULT 0,
    CoolDown      INTEGER NOT NULL
                          DEFAULT 0,
    Steal         BOOLEAN DEFAULT FALSE
                          NOT NULL,
    ToolID        INTEGER NOT NULL,
    BaseObjectID  INTEGER NOT NULL,
    FOREIGN KEY (BaseObjectID) REFERENCES BaseObject (BaseObject_ID) ON DELETE CASCADE ON UPDATE CASCADE DEFERRABLE INITIALLY DEFERRED
    FOREIGN KEY (
        ToolID
    )
    REFERENCES Tool (Tool_ID) ON DELETE CASCADE
                              ON UPDATE CASCADE DEFERRABLE INITIALLY DEFERRED
);

-- Table: Skill_To_Enemy
CREATE TABLE Skill_To_Enemy(
    SkillID INTEGER NOT NULL,
    EnemyID INTEGER NOT NULL,
    TableIndex INTEGER NOT NULL,
    Response INTEGER NOT NULL,
    PRIMARY KEY (SkillID, EnemyID),
    FOREIGN KEY (SkillID) REFERENCES Skill(Skill_ID) ON DELETE CASCADE ON UPDATE CASCADE DEFERRABLE INITIALLY DEFERRED
    FOREIGN KEY (EnemyID) REFERENCES Enemy(Enemy_ID) ON DELETE CASCADE ON UPDATE CASCADE DEFERRABLE INITIALLY DEFERRED
);

-- Table: Skill_To_Player
CREATE TABLE Skill_To_Player(
    SkillID INTEGER NOT NULL,
    PlayerID INTEGER NOT NULL,
    TableIndex INTEGER NOT NULL,
    Response INTEGER NOT NULL,
    PRIMARY KEY (SkillID, PlayerID),
    FOREIGN KEY (SkillID) REFERENCES Skill(Skill_ID) ON DELETE CASCADE ON UPDATE CASCADE DEFERRABLE INITIALLY DEFERRED
    FOREIGN KEY (PlayerID) REFERENCES Player(Player_ID) ON DELETE CASCADE ON UPDATE CASCADE DEFERRABLE INITIALLY DEFERRED
);

-- Table: State
CREATE TABLE State (State_ID INTEGER PRIMARY KEY NOT NULL UNIQUE, HPRegen INTEGER NOT NULL DEFAULT 0, SPRegen INTEGER NOT NULL DEFAULT 0, Stun BOOLEAN NOT NULL DEFAULT FALSE, TurnEnd1 INTEGER NOT NULL, TurnEnd2 INTEGER NOT NULL, TurnEndSequence INTEGER NOT NULL DEFAULT 0, GetHitRemove INTEGER NOT NULL DEFAULT 0, MaxStack INTEGER NOT NULL DEFAULT 0, ComboDifficulty INTEGER NOT NULL DEFAULT 100, Counter INTEGER NOT NULL DEFAULT 0, Reflect INTEGER NOT NULL DEFAULT 0, Petrify BOOLEAN NOT NULL DEFAULT FALSE, ExtraTurns INTEGER NOT NULL DEFAULT 0, PhysicalDamageRate INTEGER NOT NULL DEFAULT 100, MagicalDamageRate INTEGER NOT NULL DEFAULT 100, StepsToRemove INTEGER NOT NULL DEFAULT 0, RetliateEffect INTEGER NOT NULL DEFAULT 0, DisabledToolType1 INTEGER, DisabledToolType2 INTEGER, BaseObjectID INTEGER NOT NULL, StatModifiers INTEGER NOT NULL, FOREIGN KEY (BaseObjectID) REFERENCES BaseObject (BaseObject_ID) ON UPDATE CASCADE ON DELETE CASCADE, FOREIGN KEY (StatModifiers) REFERENCES Stats (Stats_ID));

-- Table: Stats
CREATE TABLE Stats (
    Stats_ID INTEGER PRIMARY KEY
                     NOT NULL
                     UNIQUE,
    HP       DOUBLE  NOT NULL,
    Atk      DOUBLE  NOT NULL,
    Def      DOUBLE  NOT NULL,
    Map      DOUBLE  NOT NULL,
    Mar      DOUBLE  NOT NULL,
    Spd      DOUBLE  NOT NULL,
    Tec      DOUBLE  NOT NULL,
    Luk      DOUBLE  NOT NULL,
    Acc      DOUBLE  NOT NULL
                     DEFAULT 100,
    Eva      DOUBLE  NOT NULL
                     DEFAULT 100,
    Crt      DOUBLE  NOT NULL
                     DEFAULT 100,
    Cev      DOUBLE  NOT NULL
                     DEFAULT 100
);
INSERT INTO Stats (Stats_ID, HP, Atk, Def, Map, Mar, Spd, Tec, Luk, Acc, Eva, Crt, Cev) VALUES (1, 3, 3, 3, 2.5, 2.5, 3, 3, 3, 100, 100, 100, 100);
INSERT INTO Stats (Stats_ID, HP, Atk, Def, Map, Mar, Spd, Tec, Luk, Acc, Eva, Crt, Cev) VALUES (2, 6, 6, 6, 6, 6, 6, 6, 6, 100, 100, 100, 100);
INSERT INTO Stats (Stats_ID, HP, Atk, Def, Map, Mar, Spd, Tec, Luk, Acc, Eva, Crt, Cev) VALUES (3, 5.5, 5, 3.5, 1.5, 1, 2, 1, 2, 100, 100, 100, 100);
INSERT INTO Stats (Stats_ID, HP, Atk, Def, Map, Mar, Spd, Tec, Luk, Acc, Eva, Crt, Cev) VALUES (4, 8.5, 7, 6.5, 4, 6, 4, 4, 6.5, 100, 100, 100, 100);
INSERT INTO Stats (Stats_ID, HP, Atk, Def, Map, Mar, Spd, Tec, Luk, Acc, Eva, Crt, Cev) VALUES (5, 7.5, 8.5, 5.5, 4, 4, 6.5, 4, 6, 100, 100, 100, 100);
INSERT INTO Stats (Stats_ID, HP, Atk, Def, Map, Mar, Spd, Tec, Luk, Acc, Eva, Crt, Cev) VALUES (6, 1.5, 3.5, 2, 1, 1.5, 5.5, 5, 3.5, 100, 100, 100, 100);
INSERT INTO Stats (Stats_ID, HP, Atk, Def, Map, Mar, Spd, Tec, Luk, Acc, Eva, Crt, Cev) VALUES (7, 5, 6.5, 6, 4.5, 4, 7, 8.5, 7, 100, 100, 100, 100);
INSERT INTO Stats (Stats_ID, HP, Atk, Def, Map, Mar, Spd, Tec, Luk, Acc, Eva, Crt, Cev) VALUES (8, 1, 0, 1, 5.5, 3, 4.5, 4, 3.5, 100, 100, 100, 100);
INSERT INTO Stats (Stats_ID, HP, Atk, Def, Map, Mar, Spd, Tec, Luk, Acc, Eva, Crt, Cev) VALUES (9, 5.5, 4, 4, 8.5, 7, 7, 6.5, 6.5, 100, 100, 100, 100);
INSERT INTO Stats (Stats_ID, HP, Atk, Def, Map, Mar, Spd, Tec, Luk, Acc, Eva, Crt, Cev) VALUES (10, 4, 7.5, 5, 7.5, 4.5, 8.5, 6, 4, 100, 100, 100, 100);
INSERT INTO Stats (Stats_ID, HP, Atk, Def, Map, Mar, Spd, Tec, Luk, Acc, Eva, Crt, Cev) VALUES (11, 3, 4, 5.5, 3.5, 4.5, 1, 2, 1, 100, 100, 100, 100);
INSERT INTO Stats (Stats_ID, HP, Atk, Def, Map, Mar, Spd, Tec, Luk, Acc, Eva, Crt, Cev) VALUES (12, 6, 7, 8.5, 6, 7, 5, 5.5, 5.5, 100, 100, 100, 100);
INSERT INTO Stats (Stats_ID, HP, Atk, Def, Map, Mar, Spd, Tec, Luk, Acc, Eva, Crt, Cev) VALUES (13, 2, 1, 1.5, 3.5, 5, 3, 2.5, 5.5, 100, 100, 100, 100);
INSERT INTO Stats (Stats_ID, HP, Atk, Def, Map, Mar, Spd, Tec, Luk, Acc, Eva, Crt, Cev) VALUES (14, 6, 4, 6, 6, 8.5, 5.5, 6, 7.5, 100, 100, 100, 100);
INSERT INTO Stats (Stats_ID, HP, Atk, Def, Map, Mar, Spd, Tec, Luk, Acc, Eva, Crt, Cev) VALUES (15, 7, 5.5, 7.5, 6.5, 5.5, 4.5, 6.5, 5, 100, 100, 100, 100);
INSERT INTO Stats (Stats_ID, HP, Atk, Def, Map, Mar, Spd, Tec, Luk, Acc, Eva, Crt, Cev) VALUES (16, 4.5, 2.5, 4, 0, 3.5, 1.5, 3.5, 2.5, 100, 100, 100, 100);
INSERT INTO Stats (Stats_ID, HP, Atk, Def, Map, Mar, Spd, Tec, Luk, Acc, Eva, Crt, Cev) VALUES (17, 6.5, 4.5, 7, 5, 6.5, 5, 5, 6, 100, 100, 100, 100);
INSERT INTO Stats (Stats_ID, HP, Atk, Def, Map, Mar, Spd, Tec, Luk, Acc, Eva, Crt, Cev) VALUES (18, 3, 3, 2.5, 2.5, 2, 2.5, 4.5, 3, 100, 100, 100, 100);
INSERT INTO Stats (Stats_ID, HP, Atk, Def, Map, Mar, Spd, Tec, Luk, Acc, Eva, Crt, Cev) VALUES (19, 5.5, 6.5, 5.5, 6.5, 5.5, 5, 7.5, 5.5, 100, 100, 100, 100);
INSERT INTO Stats (Stats_ID, HP, Atk, Def, Map, Mar, Spd, Tec, Luk, Acc, Eva, Crt, Cev) VALUES (20, 5, 6, 4.5, 5, 7.5, 6.5, 7, 8.5, 100, 100, 100, 100);

-- Table: Tool
CREATE TABLE Tool (
    Tool_ID         INTEGER PRIMARY KEY
                            NOT NULL
                            UNIQUE,
    Type            TEXT    NOT NULL,
    Formula         INTEGER DEFAULT NULL,
    HPSPModType     TEXT    NOT NULL
                            DEFAULT NULL,
    HPAmount        INTEGER NOT NULL
                            DEFAULT (0),
    SPAmount        INTEGER NOT NULL
                            DEFAULT (0),
    HPPercent       INTEGER NOT NULL
                            DEFAULT (0),
    SPPercent       INTEGER NOT NULL
                            DEFAULT (0),
    HPRecoil        INTEGER NOT NULL
                            DEFAULT 0,
    Scope           TEXT    NOT NULL
                            DEFAULT NULL,
    ConsecutiveActs INTEGER NOT NULL
                            DEFAULT (1),
    RandomActs      INTEGER NOT NULL
                            DEFAULT (0),
    Element         INTEGER DEFAULT NULL,
    Power           INTEGER NOT NULL
                            DEFAULT 100,
    Accuracy        INTEGER NOT NULL
                            DEFAULT 100,
    CriticalRate    INTEGER NOT NULL
                            DEFAULT (100),
    Priority        INTEGER NOT NULL
                            DEFAULT 0,
    ClassExclusive1 INTEGER REFERENCES BattlerClass ON DELETE SET NULL,
    ClassExclusive2 INTEGER REFERENCES BattlerClass ON DELETE SET NULL
);

-- Table: Tool_To_State_Give
CREATE TABLE Tool_To_State_Give(
    ToolID INTEGER NOT NULL,
    StateID INTEGER NOT NULL,
    TableIndex INTEGER NOT NULL,
    Chance INTEGER NOT NULL,
    PRIMARY KEY (ToolID, StateID),
    FOREIGN KEY (ToolID) REFERENCES Tool(Tool_ID) ON DELETE CASCADE ON UPDATE CASCADE DEFERRABLE INITIALLY DEFERRED
    FOREIGN KEY (StateID) REFERENCES State(State_ID) ON DELETE CASCADE ON UPDATE CASCADE DEFERRABLE INITIALLY DEFERRED
);

-- Table: Tool_To_State_Receive
CREATE TABLE Tool_To_State_Receive (
    ToolID     INTEGER NOT NULL,
    StateID    INTEGER NOT NULL,
    TableIndex INTEGER NOT NULL,
    Chance     INTEGER NOT NULL,
    PRIMARY KEY (
        ToolID,
        StateID
    ),
    FOREIGN KEY (
        ToolID
    )
    REFERENCES Tool ON UPDATE CASCADE
                    ON DELETE CASCADE DEFERRABLE INITIALLY DEFERRED,
    FOREIGN KEY (
        StateID
    )
    REFERENCES State ON UPDATE CASCADE
                     ON DELETE CASCADE DEFERRABLE INITIALLY DEFERRED
);

-- Table: TypesLists
CREATE TABLE TypesLists (
    List_Type CHAR (20) NOT NULL,
    List_ID   INTEGER   NOT NULL,
    Name      CHAR (20),
    PRIMARY KEY (
        List_Type,
        List_ID
    )
);
INSERT INTO TypesLists (List_Type, List_ID, Name) VALUES ('Elements', 0, 'Normal');
INSERT INTO TypesLists (List_Type, List_ID, Name) VALUES ('Elements', 1, 'Fire');
INSERT INTO TypesLists (List_Type, List_ID, Name) VALUES ('Elements', 2, 'Ice');
INSERT INTO TypesLists (List_Type, List_ID, Name) VALUES ('Elements', 3, 'Earth');
INSERT INTO TypesLists (List_Type, List_ID, Name) VALUES ('Elements', 4, 'Wind');
INSERT INTO TypesLists (List_Type, List_ID, Name) VALUES ('Elements', 5, 'Thunder');
INSERT INTO TypesLists (List_Type, List_ID, Name) VALUES ('Elements', 6, 'Light');
INSERT INTO TypesLists (List_Type, List_ID, Name) VALUES ('Weapon Types', 0, 'Sword');
INSERT INTO TypesLists (List_Type, List_ID, Name) VALUES ('Weapon Types', 1, 'Hammer');
INSERT INTO TypesLists (List_Type, List_ID, Name) VALUES ('Weapon Types', 2, 'Gloves');
INSERT INTO TypesLists (List_Type, List_ID, Name) VALUES ('Weapon Types', 3, 'Gun');
INSERT INTO TypesLists (List_Type, List_ID, Name) VALUES ('Weapon Types', 4, 'Tools');
INSERT INTO TypesLists (List_Type, List_ID, Name) VALUES ('Tool Formulas', 0, 'Physical Standard');
INSERT INTO TypesLists (List_Type, List_ID, Name) VALUES ('Tool Formulas', 1, 'Magical Standard');
INSERT INTO TypesLists (List_Type, List_ID, Name) VALUES ('Tool Types', 0, 'Physical Offense');
INSERT INTO TypesLists (List_Type, List_ID, Name) VALUES ('Tool Types', 1, 'Physical Defense');
INSERT INTO TypesLists (List_Type, List_ID, Name) VALUES ('Tool Types', 2, 'Magical Offense');
INSERT INTO TypesLists (List_Type, List_ID, Name) VALUES ('Tool Types', 3, 'Magical Defense');
INSERT INTO TypesLists (List_Type, List_ID, Name) VALUES ('Tool Types', 4, 'General Offense');
INSERT INTO TypesLists (List_Type, List_ID, Name) VALUES ('Tool Types', 5, 'General Defense');
INSERT INTO TypesLists (List_Type, List_ID, Name) VALUES ('Elements', 7, 'Dark');
INSERT INTO TypesLists (List_Type, List_ID, Name) VALUES ('Tool Formulas', 2, 'Mixed Standard');

-- Table: Weapon
CREATE TABLE Weapon (
    Weapon_ID    INTEGER PRIMARY KEY
                         NOT NULL
                         UNIQUE,
    Type         INTEGER NOT NULL,
    Range        INTEGER NOT NULL
                         DEFAULT 6,
    GoThrough    BOOLEAN NOT NULL
                         DEFAULT FALSE,
    ToolID       INTEGER NOT NULL,
    BaseObjectID INTEGER NOT NULL,
    FOREIGN KEY (BaseObjectID) REFERENCES BaseObject (BaseObject_ID) ON DELETE CASCADE ON UPDATE CASCADE DEFERRABLE INITIALLY DEFERRED,
    FOREIGN KEY (
        ToolID
    )
    REFERENCES Tool(Tool_ID) ON UPDATE CASCADE ON DELETE CASCADE DEFERRABLE INITIALLY DEFERRED
);

COMMIT TRANSACTION;
PRAGMA foreign_keys = on;

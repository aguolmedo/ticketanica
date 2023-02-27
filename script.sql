create table direcciones
(
    id_direccion int auto_increment
        primary key,
    calle_nro    int         null,
    local_name   varchar(45) not null,
    calle_name   varchar(45) not null,
    ciudad_name  varchar(45) null
);

create table users
(
    email      varchar(255)                          not null
        primary key,
    password   char(120)                             not null,
    created_at timestamp default current_timestamp() not null,
    constraint email
        unique (email)
);

create table eventos
(
    id_evento         int auto_increment
        primary key,
    evento_name       varchar(45)  not null,
    artista_name      varchar(45)  null,
    id_direccion      int          not null,
    capacidad_maxima  int          not null,
    email_organizador varchar(255) null,
    evento_fecha      datetime     not null,
    constraint eventos_direcciones_id_direccion_fk
        foreign key (id_direccion) references direcciones (id_direccion),
    constraint eventos_users_email_fk
        foreign key (email_organizador) references users (email)
);

create table entradas
(
    id_entrada int auto_increment
        primary key,
    codigoQR   varchar(120)                          null,
    usada      tinyint(1)                            not null,
    evento_id  int                                   not null,
    created_at timestamp default current_timestamp() null,
    constraint entradas_eventos_id_evento_fk
        foreign key (evento_id) references eventos (id_evento)
            on delete cascade
);

create table reset_tokens
(
    id         int auto_increment
        primary key,
    user_email varchar(255) not null,
    token      char(64)     not null,
    expires_at timestamp    not null,
    constraint reset_tokens_users_email_fk
        foreign key (user_email) references users (email)
);

create definer = root@localhost event eliminacion_eventos_pasados on schedule
    every '1' DAY
        starts '2023-02-16 19:39:06'
    on completion preserve
    enable
    do
    begin
    delete from eventos where evento_fecha <= DATE_SUB(NOW(), INTERVAL 2 DAY );
end;

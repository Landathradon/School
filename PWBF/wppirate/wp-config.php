<?php
/**
 * The base configuration for WordPress
 *
 * The wp-config.php creation script uses this file during the
 * installation. You don't have to use the web site, you can
 * copy this file to "wp-config.php" and fill in the values.
 *
 * This file contains the following configurations:
 *
 * * MySQL settings
 * * Secret keys
 * * Database table prefix
 * * ABSPATH
 *
 * @link https://wordpress.org/support/article/editing-wp-config-php/
 *
 * @package WordPress
 */

// ** MySQL settings - You can get this info from your web host ** //
/** The name of the database for WordPress */
define( 'DB_NAME', 'pirate_db' );

/** MySQL database username */
define( 'DB_USER', 'pirateadmin' );

/** MySQL database password */
define( 'DB_PASSWORD', 'Pirate4$&Gold' );

/** MySQL hostname */
define( 'DB_HOST', 'localhost' );

/** Database Charset to use in creating database tables. */
define( 'DB_CHARSET', 'utf8mb4' );

/** The Database Collate type. Don't change this if in doubt. */
define( 'DB_COLLATE', '' );

/**#@+
 * Authentication Unique Keys and Salts.
 *
 * Change these to different unique phrases!
 * You can generate these using the {@link https://api.wordpress.org/secret-key/1.1/salt/ WordPress.org secret-key service}
 * You can change these at any point in time to invalidate all existing cookies. This will force all users to have to log in again.
 *
 * @since 2.6.0
 */
define( 'AUTH_KEY',         'Q+LZI-_B13Ba^!uT}X ZZ7J7yZ/fT5,3FvWv]oxn.cLqJ|BZ:oi@FH-K;#o;0i?p' );
define( 'SECURE_AUTH_KEY',  'CWN?Car6S=h45Z3%I0&RsZ=&W)SE*:0E:c/ab@Hn@r1%P(tpozS<*s%A-MVweS1q' );
define( 'LOGGED_IN_KEY',    '*6!_es%ZUC(Qk7[7RSIp_#H+NfYOjtkz.Xco2w9%ZbHcn;sn:lqas*wru$gPQt<E' );
define( 'NONCE_KEY',        '~bm|lEJF-}Oqbp*jfg31%efr>qTZ;V&z1F%Az#vqzyT w0*;X@SCZz+HO_E`LbTx' );
define( 'AUTH_SALT',        'RqdG!hQieY/>Xb-dBum/<Gz[FZz3^FDXK1)>yA{7$Cy0}zd4tEQX;z<kD^*QwL4z' );
define( 'SECURE_AUTH_SALT', 'Qh<lY7,qK,B]yf~vgO^n|#o`D!UUZBR:VTLA9Yen__DOnes@]W,rK3~aYFx[qm-`' );
define( 'LOGGED_IN_SALT',   '7eaikB@W@#Ip4%6Z-;%yws*[,:~Nm?[ #bnfmu!L%z$FyaBedO!]>JIquXT1t4sq' );
define( 'NONCE_SALT',       'dzx?;pJzD G,B+vO]P26*t;,}0L%_ /:#9P@*WEv Su>R#uBw<!8qd2?J(U.oGN,' );

/**#@-*/

/**
 * WordPress Database Table prefix.
 *
 * You can have multiple installations in one database if you give each
 * a unique prefix. Only numbers, letters, and underscores please!
 */
$table_prefix = 'wp_';

/**
 * For developers: WordPress debugging mode.
 *
 * Change this to true to enable the display of notices during development.
 * It is strongly recommended that plugin and theme developers use WP_DEBUG
 * in their development environments.
 *
 * For information on other constants that can be used for debugging,
 * visit the documentation.
 *
 * @link https://wordpress.org/support/article/debugging-in-wordpress/
 */
define( 'WP_DEBUG', false );

/* That's all, stop editing! Happy publishing. */

/** Absolute path to the WordPress directory. */
if ( ! defined( 'ABSPATH' ) ) {
	define( 'ABSPATH', __DIR__ . '/' );
}

/** Sets up WordPress vars and included files. */
require_once ABSPATH . 'wp-settings.php';

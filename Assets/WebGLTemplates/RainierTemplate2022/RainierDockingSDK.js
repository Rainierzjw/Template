return !
function(e, t) {
	"object" == typeof exports && "undefined" != typeof module ? t(exports) : "function" == typeof define && define.amd ? define(["exports"], t) : t((e = "undefined" != typeof globalThis ? globalThis: e || self).RainierDockingSDK = {})
} (this, (function(e) {
	"use strict";
	var t = "undefined" != typeof globalThis ? globalThis: "undefined" != typeof window ? window: "undefined" != typeof global ? global: "undefined" != typeof self ? self: {},
	n = {
		exports: {}
	}; !
	function(e) { !
		function(t, n) {
			e.exports = t.document ? n(t, !0) : function(e) {
				if (!e.document) throw new Error("jQuery requires a window with a document");
				return n(e)
			}
		} ("undefined" != typeof window ? window: t, (function(e, t) {
			var n = [],
			r = Object.getPrototypeOf,
			o = n.slice,
			i = function(e) {
				return n.concat.apply([], e)
			},
			a = n.push,
			s = n.indexOf,
			u = {},
			l = u.toString,
			c = u.hasOwnProperty,
			f = c.toString,
			p = f.call(Object),
			d = {},
			h = function(e) {
				return "function" == typeof e && "number" != typeof e.nodeType && "function" != typeof e.item
			},
			g = function(e) {
				return null != e && e === e.window
			},
			v = e.document,
			y = {
				type: !0,
				src: !0,
				nonce: !0,
				noModule: !0
			};
			function m(e, t, n) {
				var r, o, i = (n = n || v).createElement("script");
				if (i.text = e, t) for (r in y)(o = t[r] || t.getAttribute && t.getAttribute(r)) && i.setAttribute(r, o);
				n.head.appendChild(i).parentNode.removeChild(i)
			}
			function x(e) {
				return null == e ? e + "": "object" == typeof e || "function" == typeof e ? u[l.call(e)] || "object": typeof e
			}
			var b = "3.6.0",
			w = function(e, t) {
				return new w.fn.init(e, t)
			};
			function T(e) {
				var t = !!e && "length" in e && e.length,
				n = x(e);
				return ! h(e) && !g(e) && ("array" === n || 0 === t || "number" == typeof t && t > 0 && t - 1 in e)
			}
			w.fn = w.prototype = {
				jquery: b,
				constructor: w,
				length: 0,
				toArray: function() {
					return o.call(this)
				},
				get: function(e) {
					return null == e ? o.call(this) : e < 0 ? this[e + this.length] : this[e]
				},
				pushStack: function(e) {
					var t = w.merge(this.constructor(), e);
					return t.prevObject = this,
					t
				},
				each: function(e) {
					return w.each(this, e)
				},
				map: function(e) {
					return this.pushStack(w.map(this, (function(t, n) {
						return e.call(t, n, t)
					})))
				},
				slice: function() {
					return this.pushStack(o.apply(this, arguments))
				},
				first: function() {
					return this.eq(0)
				},
				last: function() {
					return this.eq(-1)
				},
				even: function() {
					return this.pushStack(w.grep(this, (function(e, t) {
						return (t + 1) % 2
					})))
				},
				odd: function() {
					return this.pushStack(w.grep(this, (function(e, t) {
						return t % 2
					})))
				},
				eq: function(e) {
					var t = this.length,
					n = +e + (e < 0 ? t: 0);
					return this.pushStack(n >= 0 && n < t ? [this[n]] : [])
				},
				end: function() {
					return this.prevObject || this.constructor()
				},
				push: a,
				sort: n.sort,
				splice: n.splice
			},
			w.extend = w.fn.extend = function() {
				var e, t, n, r, o, i, a = arguments[0] || {},
				s = 1,
				u = arguments.length,
				l = !1;
				for ("boolean" == typeof a && (l = a, a = arguments[s] || {},
				s++), "object" == typeof a || h(a) || (a = {}), s === u && (a = this, s--); s < u; s++) if (null != (e = arguments[s])) for (t in e) r = e[t],
				"__proto__" !== t && a !== r && (l && r && (w.isPlainObject(r) || (o = Array.isArray(r))) ? (n = a[t], i = o && !Array.isArray(n) ? [] : o || w.isPlainObject(n) ? n: {},
				o = !1, a[t] = w.extend(l, i, r)) : void 0 !== r && (a[t] = r));
				return a
			},
			w.extend({
				expando: "jQuery" + (b + Math.random()).replace(/\D/g, ""),
				isReady: !0,
				error: function(e) {
					throw new Error(e)
				},
				noop: function() {},
				isPlainObject: function(e) {
					var t, n;
					return ! (!e || "[object Object]" !== l.call(e)) && (!(t = r(e)) || "function" == typeof(n = c.call(t, "constructor") && t.constructor) && f.call(n) === p)
				},
				isEmptyObject: function(e) {
					var t;
					for (t in e) return ! 1;
					return ! 0
				},
				globalEval: function(e, t, n) {
					m(e, {
						nonce: t && t.nonce
					},
					n)
				},
				each: function(e, t) {
					var n, r = 0;
					if (T(e)) for (n = e.length; r < n && !1 !== t.call(e[r], r, e[r]); r++);
					else for (r in e) if (!1 === t.call(e[r], r, e[r])) break;
					return e
				},
				makeArray: function(e, t) {
					var n = t || [];
					return null != e && (T(Object(e)) ? w.merge(n, "string" == typeof e ? [e] : e) : a.call(n, e)),
					n
				},
				inArray: function(e, t, n) {
					return null == t ? -1 : s.call(t, e, n)
				},
				merge: function(e, t) {
					for (var n = +t.length,
					r = 0,
					o = e.length; r < n; r++) e[o++] = t[r];
					return e.length = o,
					e
				},
				grep: function(e, t, n) {
					for (var r = [], o = 0, i = e.length, a = !n; o < i; o++) ! t(e[o], o) !== a && r.push(e[o]);
					return r
				},
				map: function(e, t, n) {
					var r, o, a = 0,
					s = [];
					if (T(e)) for (r = e.length; a < r; a++) null != (o = t(e[a], a, n)) && s.push(o);
					else for (a in e) null != (o = t(e[a], a, n)) && s.push(o);
					return i(s)
				},
				guid: 1,
				support: d
			}),
			"function" == typeof Symbol && (w.fn[Symbol.iterator] = n[Symbol.iterator]),
			w.each("Boolean Number String Function Array Date RegExp Object Error Symbol".split(" "), (function(e, t) {
				u["[object " + t + "]"] = t.toLowerCase()
			}));
			var S = function(e) {
				var t, n, r, o, i, a, s, u, l, c, f, p, d, h, g, v, y, m, x, b = "sizzle" + 1 * new Date,
				w = e.document,
				T = 0,
				S = 0,
				C = ue(),
				D = ue(),
				N = ue(),
				E = ue(),
				O = function(e, t) {
					return e === t && (f = !0),
					0
				},
				k = {}.hasOwnProperty,
				j = [],
				A = j.pop,
				I = j.push,
				L = j.push,
				q = j.slice,
				R = function(e, t) {
					for (var n = 0,
					r = e.length; n < r; n++) if (e[n] === t) return n;
					return - 1
				},
				H = "checked|selected|async|autofocus|autoplay|controls|defer|disabled|hidden|ismap|loop|multiple|open|readonly|required|scoped",
				F = "[\\x20\\t\\r\\n\\f]",
				P = "(?:\\\\[\\da-fA-F]{1,6}[\\x20\\t\\r\\n\\f]?|\\\\[^\\r\\n\\f]|[\\w-]|[^\0-\\x7f])+",
				M = "\\[[\\x20\\t\\r\\n\\f]*(" + P + ")(?:" + F + "*([*^$|!~]?=)" + F + "*(?:'((?:\\\\.|[^\\\\'])*)'|\"((?:\\\\.|[^\\\\\"])*)\"|(" + P + "))|)" + F + "*\\]",
				B = ":(" + P + ")(?:\\((('((?:\\\\.|[^\\\\'])*)'|\"((?:\\\\.|[^\\\\\"])*)\")|((?:\\\\.|[^\\\\()[\\]]|" + M + ")*)|.*)\\)|)",
				$ = new RegExp(F + "+", "g"),
				W = new RegExp("^[\\x20\\t\\r\\n\\f]+|((?:^|[^\\\\])(?:\\\\.)*)[\\x20\\t\\r\\n\\f]+$", "g"),
				_ = new RegExp("^[\\x20\\t\\r\\n\\f]*,[\\x20\\t\\r\\n\\f]*"),
				U = new RegExp("^[\\x20\\t\\r\\n\\f]*([>+~]|[\\x20\\t\\r\\n\\f])[\\x20\\t\\r\\n\\f]*"),
				J = new RegExp(F + "|>"),
				z = new RegExp(B),
				X = new RegExp("^" + P + "$"),
				G = {
					ID: new RegExp("^#(" + P + ")"),
					CLASS: new RegExp("^\\.(" + P + ")"),
					TAG: new RegExp("^(" + P + "|[*])"),
					ATTR: new RegExp("^" + M),
					PSEUDO: new RegExp("^" + B),
					CHILD: new RegExp("^:(only|first|last|nth|nth-last)-(child|of-type)(?:\\([\\x20\\t\\r\\n\\f]*(even|odd|(([+-]|)(\\d*)n|)[\\x20\\t\\r\\n\\f]*(?:([+-]|)[\\x20\\t\\r\\n\\f]*(\\d+)|))[\\x20\\t\\r\\n\\f]*\\)|)", "i"),
					bool: new RegExp("^(?:" + H + ")$", "i"),
					needsContext: new RegExp("^[\\x20\\t\\r\\n\\f]*[>+~]|:(even|odd|eq|gt|lt|nth|first|last)(?:\\([\\x20\\t\\r\\n\\f]*((?:-\\d)?\\d*)[\\x20\\t\\r\\n\\f]*\\)|)(?=[^-]|$)", "i")
				},
				V = /HTML$/i,
				Y = /^(?:input|select|textarea|button)$/i,
				Q = /^h\d$/i,
				K = /^[^{]+\{\s*\[native \w/,
				Z = /^(?:#([\w-]+)|(\w+)|\.([\w-]+))$/,
				ee = /[+~]/,
				te = new RegExp("\\\\[\\da-fA-F]{1,6}[\\x20\\t\\r\\n\\f]?|\\\\([^\\r\\n\\f])", "g"),
				ne = function(e, t) {
					var n = "0x" + e.slice(1) - 65536;
					return t || (n < 0 ? String.fromCharCode(n + 65536) : String.fromCharCode(n >> 10 | 55296, 1023 & n | 56320))
				},
				re = /([\0-\x1f\x7f]|^-?\d)|^-$|[^\0-\x1f\x7f-\uFFFF\w-]/g,
				oe = function(e, t) {
					return t ? "\0" === e ? "�": e.slice(0, -1) + "\\" + e.charCodeAt(e.length - 1).toString(16) + " ": "\\" + e
				},
				ie = function() {
					p()
				},
				ae = be((function(e) {
					return ! 0 === e.disabled && "fieldset" === e.nodeName.toLowerCase()
				}), {
					dir: "parentNode",
					next: "legend"
				});
				try {
					L.apply(j = q.call(w.childNodes), w.childNodes),
					j[w.childNodes.length].nodeType
				} catch(e) {
					L = {
						apply: j.length ?
						function(e, t) {
							I.apply(e, q.call(t))
						}: function(e, t) {
							for (var n = e.length,
							r = 0; e[n++] = t[r++];);
							e.length = n - 1
						}
					}
				}
				function se(e, t, r, o) {
					var i, s, l, c, f, h, y, m = t && t.ownerDocument,
					w = t ? t.nodeType: 9;
					if (r = r || [], "string" != typeof e || !e || 1 !== w && 9 !== w && 11 !== w) return r;
					if (!o && (p(t), t = t || d, g)) {
						if (11 !== w && (f = Z.exec(e))) if (i = f[1]) {
							if (9 === w) {
								if (! (l = t.getElementById(i))) return r;
								if (l.id === i) return r.push(l),
								r
							} else if (m && (l = m.getElementById(i)) && x(t, l) && l.id === i) return r.push(l),
							r
						} else {
							if (f[2]) return L.apply(r, t.getElementsByTagName(e)),
							r;
							if ((i = f[3]) && n.getElementsByClassName && t.getElementsByClassName) return L.apply(r, t.getElementsByClassName(i)),
							r
						}
						if (n.qsa && !E[e + " "] && (!v || !v.test(e)) && (1 !== w || "object" !== t.nodeName.toLowerCase())) {
							if (y = e, m = t, 1 === w && (J.test(e) || U.test(e))) {
								for ((m = ee.test(e) && ye(t.parentNode) || t) === t && n.scope || ((c = t.getAttribute("id")) ? c = c.replace(re, oe) : t.setAttribute("id", c = b)), s = (h = a(e)).length; s--;) h[s] = (c ? "#" + c: ":scope") + " " + xe(h[s]);
								y = h.join(",")
							}
							try {
								return L.apply(r, m.querySelectorAll(y)),
								r
							} catch(t) {
								E(e, !0)
							} finally {
								c === b && t.removeAttribute("id")
							}
						}
					}
					return u(e.replace(W, "$1"), t, r, o)
				}
				function ue() {
					var e = [];
					return function t(n, o) {
						return e.push(n + " ") > r.cacheLength && delete t[e.shift()],
						t[n + " "] = o
					}
				}
				function le(e) {
					return e[b] = !0,
					e
				}
				function ce(e) {
					var t = d.createElement("fieldset");
					try {
						return !! e(t)
					} catch(e) {
						return ! 1
					} finally {
						t.parentNode && t.parentNode.removeChild(t),
						t = null
					}
				}
				function fe(e, t) {
					for (var n = e.split("|"), o = n.length; o--;) r.attrHandle[n[o]] = t
				}
				function pe(e, t) {
					var n = t && e,
					r = n && 1 === e.nodeType && 1 === t.nodeType && e.sourceIndex - t.sourceIndex;
					if (r) return r;
					if (n) for (; n = n.nextSibling;) if (n === t) return - 1;
					return e ? 1 : -1
				}
				function de(e) {
					return function(t) {
						return "input" === t.nodeName.toLowerCase() && t.type === e
					}
				}
				function he(e) {
					return function(t) {
						var n = t.nodeName.toLowerCase();
						return ("input" === n || "button" === n) && t.type === e
					}
				}
				function ge(e) {
					return function(t) {
						return "form" in t ? t.parentNode && !1 === t.disabled ? "label" in t ? "label" in t.parentNode ? t.parentNode.disabled === e: t.disabled === e: t.isDisabled === e || t.isDisabled !== !e && ae(t) === e: t.disabled === e: "label" in t && t.disabled === e
					}
				}
				function ve(e) {
					return le((function(t) {
						return t = +t,
						le((function(n, r) {
							for (var o, i = e([], n.length, t), a = i.length; a--;) n[o = i[a]] && (n[o] = !(r[o] = n[o]))
						}))
					}))
				}
				function ye(e) {
					return e && void 0 !== e.getElementsByTagName && e
				}
				for (t in n = se.support = {},
				i = se.isXML = function(e) {
					var t = e && e.namespaceURI,
					n = e && (e.ownerDocument || e).documentElement;
					return ! V.test(t || n && n.nodeName || "HTML")
				},
				p = se.setDocument = function(e) {
					var t, o, a = e ? e.ownerDocument || e: w;
					return a != d && 9 === a.nodeType && a.documentElement ? (h = (d = a).documentElement, g = !i(d), w != d && (o = d.defaultView) && o.top !== o && (o.addEventListener ? o.addEventListener("unload", ie, !1) : o.attachEvent && o.attachEvent("onunload", ie)), n.scope = ce((function(e) {
						return h.appendChild(e).appendChild(d.createElement("div")),
						void 0 !== e.querySelectorAll && !e.querySelectorAll(":scope fieldset div").length
					})), n.attributes = ce((function(e) {
						return e.className = "i",
						!e.getAttribute("className")
					})), n.getElementsByTagName = ce((function(e) {
						return e.appendChild(d.createComment("")),
						!e.getElementsByTagName("*").length
					})), n.getElementsByClassName = K.test(d.getElementsByClassName), n.getById = ce((function(e) {
						return h.appendChild(e).id = b,
						!d.getElementsByName || !d.getElementsByName(b).length
					})), n.getById ? (r.filter.ID = function(e) {
						var t = e.replace(te, ne);
						return function(e) {
							return e.getAttribute("id") === t
						}
					},
					r.find.ID = function(e, t) {
						if (void 0 !== t.getElementById && g) {
							var n = t.getElementById(e);
							return n ? [n] : []
						}
					}) : (r.filter.ID = function(e) {
						var t = e.replace(te, ne);
						return function(e) {
							var n = void 0 !== e.getAttributeNode && e.getAttributeNode("id");
							return n && n.value === t
						}
					},
					r.find.ID = function(e, t) {
						if (void 0 !== t.getElementById && g) {
							var n, r, o, i = t.getElementById(e);
							if (i) {
								if ((n = i.getAttributeNode("id")) && n.value === e) return [i];
								for (o = t.getElementsByName(e), r = 0; i = o[r++];) if ((n = i.getAttributeNode("id")) && n.value === e) return [i]
							}
							return []
						}
					}), r.find.TAG = n.getElementsByTagName ?
					function(e, t) {
						return void 0 !== t.getElementsByTagName ? t.getElementsByTagName(e) : n.qsa ? t.querySelectorAll(e) : void 0
					}: function(e, t) {
						var n, r = [],
						o = 0,
						i = t.getElementsByTagName(e);
						if ("*" === e) {
							for (; n = i[o++];) 1 === n.nodeType && r.push(n);
							return r
						}
						return i
					},
					r.find.CLASS = n.getElementsByClassName &&
					function(e, t) {
						if (void 0 !== t.getElementsByClassName && g) return t.getElementsByClassName(e)
					},
					y = [], v = [], (n.qsa = K.test(d.querySelectorAll)) && (ce((function(e) {
						var t;
						h.appendChild(e).innerHTML = "<a id='" + b + "'></a><select id='" + b + "-\r\\' msallowcapture=''><option selected=''></option></select>",
						e.querySelectorAll("[msallowcapture^='']").length && v.push("[*^$]=[\\x20\\t\\r\\n\\f]*(?:''|\"\")"),
						e.querySelectorAll("[selected]").length || v.push("\\[[\\x20\\t\\r\\n\\f]*(?:value|" + H + ")"),
						e.querySelectorAll("[id~=" + b + "-]").length || v.push("~="),
						(t = d.createElement("input")).setAttribute("name", ""),
						e.appendChild(t),
						e.querySelectorAll("[name='']").length || v.push("\\[[\\x20\\t\\r\\n\\f]*name[\\x20\\t\\r\\n\\f]*=[\\x20\\t\\r\\n\\f]*(?:''|\"\")"),
						e.querySelectorAll(":checked").length || v.push(":checked"),
						e.querySelectorAll("a#" + b + "+*").length || v.push(".#.+[+~]"),
						e.querySelectorAll("\\\f"),
						v.push("[\\r\\n\\f]")
					})), ce((function(e) {
						e.innerHTML = "<a href='' disabled='disabled'></a><select disabled='disabled'><option/></select>";
						var t = d.createElement("input");
						t.setAttribute("type", "hidden"),
						e.appendChild(t).setAttribute("name", "D"),
						e.querySelectorAll("[name=d]").length && v.push("name[\\x20\\t\\r\\n\\f]*[*^$|!~]?="),
						2 !== e.querySelectorAll(":enabled").length && v.push(":enabled", ":disabled"),
						h.appendChild(e).disabled = !0,
						2 !== e.querySelectorAll(":disabled").length && v.push(":enabled", ":disabled"),
						e.querySelectorAll("*,:x"),
						v.push(",.*:")
					}))), (n.matchesSelector = K.test(m = h.matches || h.webkitMatchesSelector || h.mozMatchesSelector || h.oMatchesSelector || h.msMatchesSelector)) && ce((function(e) {
						n.disconnectedMatch = m.call(e, "*"),
						m.call(e, "[s!='']:x"),
						y.push("!=", B)
					})), v = v.length && new RegExp(v.join("|")), y = y.length && new RegExp(y.join("|")), t = K.test(h.compareDocumentPosition), x = t || K.test(h.contains) ?
					function(e, t) {
						var n = 9 === e.nodeType ? e.documentElement: e,
						r = t && t.parentNode;
						return e === r || !(!r || 1 !== r.nodeType || !(n.contains ? n.contains(r) : e.compareDocumentPosition && 16 & e.compareDocumentPosition(r)))
					}: function(e, t) {
						if (t) for (; t = t.parentNode;) if (t === e) return ! 0;
						return ! 1
					},
					O = t ?
					function(e, t) {
						if (e === t) return f = !0,
						0;
						var r = !e.compareDocumentPosition - !t.compareDocumentPosition;
						return r || (1 & (r = (e.ownerDocument || e) == (t.ownerDocument || t) ? e.compareDocumentPosition(t) : 1) || !n.sortDetached && t.compareDocumentPosition(e) === r ? e == d || e.ownerDocument == w && x(w, e) ? -1 : t == d || t.ownerDocument == w && x(w, t) ? 1 : c ? R(c, e) - R(c, t) : 0 : 4 & r ? -1 : 1)
					}: function(e, t) {
						if (e === t) return f = !0,
						0;
						var n, r = 0,
						o = e.parentNode,
						i = t.parentNode,
						a = [e],
						s = [t];
						if (!o || !i) return e == d ? -1 : t == d ? 1 : o ? -1 : i ? 1 : c ? R(c, e) - R(c, t) : 0;
						if (o === i) return pe(e, t);
						for (n = e; n = n.parentNode;) a.unshift(n);
						for (n = t; n = n.parentNode;) s.unshift(n);
						for (; a[r] === s[r];) r++;
						return r ? pe(a[r], s[r]) : a[r] == w ? -1 : s[r] == w ? 1 : 0
					},
					d) : d
				},
				se.matches = function(e, t) {
					return se(e, null, null, t)
				},
				se.matchesSelector = function(e, t) {
					if (p(e), n.matchesSelector && g && !E[t + " "] && (!y || !y.test(t)) && (!v || !v.test(t))) try {
						var r = m.call(e, t);
						if (r || n.disconnectedMatch || e.document && 11 !== e.document.nodeType) return r
					} catch(e) {
						E(t, !0)
					}
					return se(t, d, null, [e]).length > 0
				},
				se.contains = function(e, t) {
					return (e.ownerDocument || e) != d && p(e),
					x(e, t)
				},
				se.attr = function(e, t) { (e.ownerDocument || e) != d && p(e);
					var o = r.attrHandle[t.toLowerCase()],
					i = o && k.call(r.attrHandle, t.toLowerCase()) ? o(e, t, !g) : void 0;
					return void 0 !== i ? i: n.attributes || !g ? e.getAttribute(t) : (i = e.getAttributeNode(t)) && i.specified ? i.value: null
				},
				se.escape = function(e) {
					return (e + "").replace(re, oe)
				},
				se.error = function(e) {
					throw new Error("Syntax error, unrecognized expression: " + e)
				},
				se.uniqueSort = function(e) {
					var t, r = [],
					o = 0,
					i = 0;
					if (f = !n.detectDuplicates, c = !n.sortStable && e.slice(0), e.sort(O), f) {
						for (; t = e[i++];) t === e[i] && (o = r.push(i));
						for (; o--;) e.splice(r[o], 1)
					}
					return c = null,
					e
				},
				o = se.getText = function(e) {
					var t, n = "",
					r = 0,
					i = e.nodeType;
					if (i) {
						if (1 === i || 9 === i || 11 === i) {
							if ("string" == typeof e.textContent) return e.textContent;
							for (e = e.firstChild; e; e = e.nextSibling) n += o(e)
						} else if (3 === i || 4 === i) return e.nodeValue
					} else for (; t = e[r++];) n += o(t);
					return n
				},
				r = se.selectors = {
					cacheLength: 50,
					createPseudo: le,
					match: G,
					attrHandle: {},
					find: {},
					relative: {
						">": {
							dir: "parentNode",
							first: !0
						},
						" ": {
							dir: "parentNode"
						},
						"+": {
							dir: "previousSibling",
							first: !0
						},
						"~": {
							dir: "previousSibling"
						}
					},
					preFilter: {
						ATTR: function(e) {
							return e[1] = e[1].replace(te, ne),
							e[3] = (e[3] || e[4] || e[5] || "").replace(te, ne),
							"~=" === e[2] && (e[3] = " " + e[3] + " "),
							e.slice(0, 4)
						},
						CHILD: function(e) {
							return e[1] = e[1].toLowerCase(),
							"nth" === e[1].slice(0, 3) ? (e[3] || se.error(e[0]), e[4] = +(e[4] ? e[5] + (e[6] || 1) : 2 * ("even" === e[3] || "odd" === e[3])), e[5] = +(e[7] + e[8] || "odd" === e[3])) : e[3] && se.error(e[0]),
							e
						},
						PSEUDO: function(e) {
							var t, n = !e[6] && e[2];
							return G.CHILD.test(e[0]) ? null: (e[3] ? e[2] = e[4] || e[5] || "": n && z.test(n) && (t = a(n, !0)) && (t = n.indexOf(")", n.length - t) - n.length) && (e[0] = e[0].slice(0, t), e[2] = n.slice(0, t)), e.slice(0, 3))
						}
					},
					filter: {
						TAG: function(e) {
							var t = e.replace(te, ne).toLowerCase();
							return "*" === e ?
							function() {
								return ! 0
							}: function(e) {
								return e.nodeName && e.nodeName.toLowerCase() === t
							}
						},
						CLASS: function(e) {
							var t = C[e + " "];
							return t || (t = new RegExp("(^|[\\x20\\t\\r\\n\\f])" + e + "(" + F + "|$)")) && C(e, (function(e) {
								return t.test("string" == typeof e.className && e.className || void 0 !== e.getAttribute && e.getAttribute("class") || "")
							}))
						},
						ATTR: function(e, t, n) {
							return function(r) {
								var o = se.attr(r, e);
								return null == o ? "!=" === t: !t || (o += "", "=" === t ? o === n: "!=" === t ? o !== n: "^=" === t ? n && 0 === o.indexOf(n) : "*=" === t ? n && o.indexOf(n) > -1 : "$=" === t ? n && o.slice(-n.length) === n: "~=" === t ? (" " + o.replace($, " ") + " ").indexOf(n) > -1 : "|=" === t && (o === n || o.slice(0, n.length + 1) === n + "-"))
							}
						},
						CHILD: function(e, t, n, r, o) {
							var i = "nth" !== e.slice(0, 3),
							a = "last" !== e.slice(-4),
							s = "of-type" === t;
							return 1 === r && 0 === o ?
							function(e) {
								return !! e.parentNode
							}: function(t, n, u) {
								var l, c, f, p, d, h, g = i !== a ? "nextSibling": "previousSibling",
								v = t.parentNode,
								y = s && t.nodeName.toLowerCase(),
								m = !u && !s,
								x = !1;
								if (v) {
									if (i) {
										for (; g;) {
											for (p = t; p = p[g];) if (s ? p.nodeName.toLowerCase() === y: 1 === p.nodeType) return ! 1;
											h = g = "only" === e && !h && "nextSibling"
										}
										return ! 0
									}
									if (h = [a ? v.firstChild: v.lastChild], a && m) {
										for (x = (d = (l = (c = (f = (p = v)[b] || (p[b] = {}))[p.uniqueID] || (f[p.uniqueID] = {}))[e] || [])[0] === T && l[1]) && l[2], p = d && v.childNodes[d]; p = ++d && p && p[g] || (x = d = 0) || h.pop();) if (1 === p.nodeType && ++x && p === t) {
											c[e] = [T, d, x];
											break
										}
									} else if (m && (x = d = (l = (c = (f = (p = t)[b] || (p[b] = {}))[p.uniqueID] || (f[p.uniqueID] = {}))[e] || [])[0] === T && l[1]), !1 === x) for (; (p = ++d && p && p[g] || (x = d = 0) || h.pop()) && ((s ? p.nodeName.toLowerCase() !== y: 1 !== p.nodeType) || !++x || (m && ((c = (f = p[b] || (p[b] = {}))[p.uniqueID] || (f[p.uniqueID] = {}))[e] = [T, x]), p !== t)););
									return (x -= o) === r || x % r == 0 && x / r >= 0
								}
							}
						},
						PSEUDO: function(e, t) {
							var n, o = r.pseudos[e] || r.setFilters[e.toLowerCase()] || se.error("unsupported pseudo: " + e);
							return o[b] ? o(t) : o.length > 1 ? (n = [e, e, "", t], r.setFilters.hasOwnProperty(e.toLowerCase()) ? le((function(e, n) {
								for (var r, i = o(e, t), a = i.length; a--;) e[r = R(e, i[a])] = !(n[r] = i[a])
							})) : function(e) {
								return o(e, 0, n)
							}) : o
						}
					},
					pseudos: {
						not: le((function(e) {
							var t = [],
							n = [],
							r = s(e.replace(W, "$1"));
							return r[b] ? le((function(e, t, n, o) {
								for (var i, a = r(e, null, o, []), s = e.length; s--;)(i = a[s]) && (e[s] = !(t[s] = i))
							})) : function(e, o, i) {
								return t[0] = e,
								r(t, null, i, n),
								t[0] = null,
								!n.pop()
							}
						})),
						has: le((function(e) {
							return function(t) {
								return se(e, t).length > 0
							}
						})),
						contains: le((function(e) {
							return e = e.replace(te, ne),
							function(t) {
								return (t.textContent || o(t)).indexOf(e) > -1
							}
						})),
						lang: le((function(e) {
							return X.test(e || "") || se.error("unsupported lang: " + e),
							e = e.replace(te, ne).toLowerCase(),
							function(t) {
								var n;
								do {
									if (n = g ? t.lang: t.getAttribute("xml:lang") || t.getAttribute("lang")) return (n = n.toLowerCase()) === e || 0 === n.indexOf(e + "-")
								} while (( t = t . parentNode ) && 1 === t.nodeType);
								return ! 1
							}
						})),
						target: function(t) {
							var n = e.location && e.location.hash;
							return n && n.slice(1) === t.id
						},
						root: function(e) {
							return e === h
						},
						focus: function(e) {
							return e === d.activeElement && (!d.hasFocus || d.hasFocus()) && !!(e.type || e.href || ~e.tabIndex)
						},
						enabled: ge(!1),
						disabled: ge(!0),
						checked: function(e) {
							var t = e.nodeName.toLowerCase();
							return "input" === t && !!e.checked || "option" === t && !!e.selected
						},
						selected: function(e) {
							return e.parentNode && e.parentNode.selectedIndex,
							!0 === e.selected
						},
						empty: function(e) {
							for (e = e.firstChild; e; e = e.nextSibling) if (e.nodeType < 6) return ! 1;
							return ! 0
						},
						parent: function(e) {
							return ! r.pseudos.empty(e)
						},
						header: function(e) {
							return Q.test(e.nodeName)
						},
						input: function(e) {
							return Y.test(e.nodeName)
						},
						button: function(e) {
							var t = e.nodeName.toLowerCase();
							return "input" === t && "button" === e.type || "button" === t
						},
						text: function(e) {
							var t;
							return "input" === e.nodeName.toLowerCase() && "text" === e.type && (null == (t = e.getAttribute("type")) || "text" === t.toLowerCase())
						},
						first: ve((function() {
							return [0]
						})),
						last: ve((function(e, t) {
							return [t - 1]
						})),
						eq: ve((function(e, t, n) {
							return [n < 0 ? n + t: n]
						})),
						even: ve((function(e, t) {
							for (var n = 0; n < t; n += 2) e.push(n);
							return e
						})),
						odd: ve((function(e, t) {
							for (var n = 1; n < t; n += 2) e.push(n);
							return e
						})),
						lt: ve((function(e, t, n) {
							for (var r = n < 0 ? n + t: n > t ? t: n; --r >= 0;) e.push(r);
							return e
						})),
						gt: ve((function(e, t, n) {
							for (var r = n < 0 ? n + t: n; ++r < t;) e.push(r);
							return e
						}))
					}
				},
				r.pseudos.nth = r.pseudos.eq, {
					radio: !0,
					checkbox: !0,
					file: !0,
					password: !0,
					image: !0
				}) r.pseudos[t] = de(t);
				for (t in {
					submit: !0,
					reset: !0
				}) r.pseudos[t] = he(t);
				function me() {}
				function xe(e) {
					for (var t = 0,
					n = e.length,
					r = ""; t < n; t++) r += e[t].value;
					return r
				}
				function be(e, t, n) {
					var r = t.dir,
					o = t.next,
					i = o || r,
					a = n && "parentNode" === i,
					s = S++;
					return t.first ?
					function(t, n, o) {
						for (; t = t[r];) if (1 === t.nodeType || a) return e(t, n, o);
						return ! 1
					}: function(t, n, u) {
						var l, c, f, p = [T, s];
						if (u) {
							for (; t = t[r];) if ((1 === t.nodeType || a) && e(t, n, u)) return ! 0
						} else for (; t = t[r];) if (1 === t.nodeType || a) if (c = (f = t[b] || (t[b] = {}))[t.uniqueID] || (f[t.uniqueID] = {}), o && o === t.nodeName.toLowerCase()) t = t[r] || t;
						else {
							if ((l = c[i]) && l[0] === T && l[1] === s) return p[2] = l[2];
							if (c[i] = p, p[2] = e(t, n, u)) return ! 0
						}
						return ! 1
					}
				}
				function we(e) {
					return e.length > 1 ?
					function(t, n, r) {
						for (var o = e.length; o--;) if (!e[o](t, n, r)) return ! 1;
						return ! 0
					}: e[0]
				}
				function Te(e, t, n, r, o) {
					for (var i, a = [], s = 0, u = e.length, l = null != t; s < u; s++)(i = e[s]) && (n && !n(i, r, o) || (a.push(i), l && t.push(s)));
					return a
				}
				function Se(e, t, n, r, o, i) {
					return r && !r[b] && (r = Se(r)),
					o && !o[b] && (o = Se(o, i)),
					le((function(i, a, s, u) {
						var l, c, f, p = [],
						d = [],
						h = a.length,
						g = i ||
						function(e, t, n) {
							for (var r = 0,
							o = t.length; r < o; r++) se(e, t[r], n);
							return n
						} (t || "*", s.nodeType ? [s] : s, []),
						v = !e || !i && t ? g: Te(g, p, e, s, u),
						y = n ? o || (i ? e: h || r) ? [] : a: v;
						if (n && n(v, y, s, u), r) for (l = Te(y, d), r(l, [], s, u), c = l.length; c--;)(f = l[c]) && (y[d[c]] = !(v[d[c]] = f));
						if (i) {
							if (o || e) {
								if (o) {
									for (l = [], c = y.length; c--;)(f = y[c]) && l.push(v[c] = f);
									o(null, y = [], l, u)
								}
								for (c = y.length; c--;)(f = y[c]) && (l = o ? R(i, f) : p[c]) > -1 && (i[l] = !(a[l] = f))
							}
						} else y = Te(y === a ? y.splice(h, y.length) : y),
						o ? o(null, a, y, u) : L.apply(a, y)
					}))
				}
				function Ce(e) {
					for (var t, n, o, i = e.length,
					a = r.relative[e[0].type], s = a || r.relative[" "], u = a ? 1 : 0, c = be((function(e) {
						return e === t
					}), s, !0), f = be((function(e) {
						return R(t, e) > -1
					}), s, !0), p = [function(e, n, r) {
						var o = !a && (r || n !== l) || ((t = n).nodeType ? c(e, n, r) : f(e, n, r));
						return t = null,
						o
					}]; u < i; u++) if (n = r.relative[e[u].type]) p = [be(we(p), n)];
					else {
						if ((n = r.filter[e[u].type].apply(null, e[u].matches))[b]) {
							for (o = ++u; o < i && !r.relative[e[o].type]; o++);
							return Se(u > 1 && we(p), u > 1 && xe(e.slice(0, u - 1).concat({
								value: " " === e[u - 2].type ? "*": ""
							})).replace(W, "$1"), n, u < o && Ce(e.slice(u, o)), o < i && Ce(e = e.slice(o)), o < i && xe(e))
						}
						p.push(n)
					}
					return we(p)
				}
				return me.prototype = r.filters = r.pseudos,
				r.setFilters = new me,
				a = se.tokenize = function(e, t) {
					var n, o, i, a, s, u, l, c = D[e + " "];
					if (c) return t ? 0 : c.slice(0);
					for (s = e, u = [], l = r.preFilter; s;) {
						for (a in n && !(o = _.exec(s)) || (o && (s = s.slice(o[0].length) || s), u.push(i = [])), n = !1, (o = U.exec(s)) && (n = o.shift(), i.push({
							value: n,
							type: o[0].replace(W, " ")
						}), s = s.slice(n.length)), r.filter) ! (o = G[a].exec(s)) || l[a] && !(o = l[a](o)) || (n = o.shift(), i.push({
							value: n,
							type: a,
							matches: o
						}), s = s.slice(n.length));
						if (!n) break
					}
					return t ? s.length: s ? se.error(e) : D(e, u).slice(0)
				},
				s = se.compile = function(e, t) {
					var n, o = [],
					i = [],
					s = N[e + " "];
					if (!s) {
						for (t || (t = a(e)), n = t.length; n--;)(s = Ce(t[n]))[b] ? o.push(s) : i.push(s);
						s = N(e,
						function(e, t) {
							var n = t.length > 0,
							o = e.length > 0,
							i = function(i, a, s, u, c) {
								var f, h, v, y = 0,
								m = "0",
								x = i && [],
								b = [],
								w = l,
								S = i || o && r.find.TAG("*", c),
								C = T += null == w ? 1 : Math.random() || .1,
								D = S.length;
								for (c && (l = a == d || a || c); m !== D && null != (f = S[m]); m++) {
									if (o && f) {
										for (h = 0, a || f.ownerDocument == d || (p(f), s = !g); v = e[h++];) if (v(f, a || d, s)) {
											u.push(f);
											break
										}
										c && (T = C)
									}
									n && ((f = !v && f) && y--, i && x.push(f))
								}
								if (y += m, n && m !== y) {
									for (h = 0; v = t[h++];) v(x, b, a, s);
									if (i) {
										if (y > 0) for (; m--;) x[m] || b[m] || (b[m] = A.call(u));
										b = Te(b)
									}
									L.apply(u, b),
									c && !i && b.length > 0 && y + t.length > 1 && se.uniqueSort(u)
								}
								return c && (T = C, l = w),
								x
							};
							return n ? le(i) : i
						} (i, o)),
						s.selector = e
					}
					return s
				},
				u = se.select = function(e, t, n, o) {
					var i, u, l, c, f, p = "function" == typeof e && e,
					d = !o && a(e = p.selector || e);
					if (n = n || [], 1 === d.length) {
						if ((u = d[0] = d[0].slice(0)).length > 2 && "ID" === (l = u[0]).type && 9 === t.nodeType && g && r.relative[u[1].type]) {
							if (! (t = (r.find.ID(l.matches[0].replace(te, ne), t) || [])[0])) return n;
							p && (t = t.parentNode),
							e = e.slice(u.shift().value.length)
						}
						for (i = G.needsContext.test(e) ? 0 : u.length; i--&&(l = u[i], !r.relative[c = l.type]);) if ((f = r.find[c]) && (o = f(l.matches[0].replace(te, ne), ee.test(u[0].type) && ye(t.parentNode) || t))) {
							if (u.splice(i, 1), !(e = o.length && xe(u))) return L.apply(n, o),
							n;
							break
						}
					}
					return (p || s(e, d))(o, t, !g, n, !t || ee.test(e) && ye(t.parentNode) || t),
					n
				},
				n.sortStable = b.split("").sort(O).join("") === b,
				n.detectDuplicates = !!f,
				p(),
				n.sortDetached = ce((function(e) {
					return 1 & e.compareDocumentPosition(d.createElement("fieldset"))
				})),
				ce((function(e) {
					return e.innerHTML = "<a href='#'></a>",
					"#" === e.firstChild.getAttribute("href")
				})) || fe("type|href|height|width", (function(e, t, n) {
					if (!n) return e.getAttribute(t, "type" === t.toLowerCase() ? 1 : 2)
				})),
				n.attributes && ce((function(e) {
					return e.innerHTML = "<input/>",
					e.firstChild.setAttribute("value", ""),
					"" === e.firstChild.getAttribute("value")
				})) || fe("value", (function(e, t, n) {
					if (!n && "input" === e.nodeName.toLowerCase()) return e.defaultValue
				})),
				ce((function(e) {
					return null == e.getAttribute("disabled")
				})) || fe(H, (function(e, t, n) {
					var r;
					if (!n) return ! 0 === e[t] ? t.toLowerCase() : (r = e.getAttributeNode(t)) && r.specified ? r.value: null
				})),
				se
			} (e);
			w.find = S,
			w.expr = S.selectors,
			w.expr[":"] = w.expr.pseudos,
			w.uniqueSort = w.unique = S.uniqueSort,
			w.text = S.getText,
			w.isXMLDoc = S.isXML,
			w.contains = S.contains,
			w.escapeSelector = S.escape;
			var C = function(e, t, n) {
				for (var r = [], o = void 0 !== n; (e = e[t]) && 9 !== e.nodeType;) if (1 === e.nodeType) {
					if (o && w(e).is(n)) break;
					r.push(e)
				}
				return r
			},
			D = function(e, t) {
				for (var n = []; e; e = e.nextSibling) 1 === e.nodeType && e !== t && n.push(e);
				return n
			},
			N = w.expr.match.needsContext;
			function E(e, t) {
				return e.nodeName && e.nodeName.toLowerCase() === t.toLowerCase()
			}
			var O = /^<([a-z][^\/\0>:\x20\t\r\n\f]*)[\x20\t\r\n\f]*\/?>(?:<\/\1>|)$/i;
			function k(e, t, n) {
				return h(t) ? w.grep(e, (function(e, r) {
					return !! t.call(e, r, e) !== n
				})) : t.nodeType ? w.grep(e, (function(e) {
					return e === t !== n
				})) : "string" != typeof t ? w.grep(e, (function(e) {
					return s.call(t, e) > -1 !== n
				})) : w.filter(t, e, n)
			}
			w.filter = function(e, t, n) {
				var r = t[0];
				return n && (e = ":not(" + e + ")"),
				1 === t.length && 1 === r.nodeType ? w.find.matchesSelector(r, e) ? [r] : [] : w.find.matches(e, w.grep(t, (function(e) {
					return 1 === e.nodeType
				})))
			},
			w.fn.extend({
				find: function(e) {
					var t, n, r = this.length,
					o = this;
					if ("string" != typeof e) return this.pushStack(w(e).filter((function() {
						for (t = 0; t < r; t++) if (w.contains(o[t], this)) return ! 0
					})));
					for (n = this.pushStack([]), t = 0; t < r; t++) w.find(e, o[t], n);
					return r > 1 ? w.uniqueSort(n) : n
				},
				filter: function(e) {
					return this.pushStack(k(this, e || [], !1))
				},
				not: function(e) {
					return this.pushStack(k(this, e || [], !0))
				},
				is: function(e) {
					return !! k(this, "string" == typeof e && N.test(e) ? w(e) : e || [], !1).length
				}
			});
			var j, A = /^(?:\s*(<[\w\W]+>)[^>]*|#([\w-]+))$/; (w.fn.init = function(e, t, n) {
				var r, o;
				if (!e) return this;
				if (n = n || j, "string" == typeof e) {
					if (! (r = "<" === e[0] && ">" === e[e.length - 1] && e.length >= 3 ? [null, e, null] : A.exec(e)) || !r[1] && t) return ! t || t.jquery ? (t || n).find(e) : this.constructor(t).find(e);
					if (r[1]) {
						if (t = t instanceof w ? t[0] : t, w.merge(this, w.parseHTML(r[1], t && t.nodeType ? t.ownerDocument || t: v, !0)), O.test(r[1]) && w.isPlainObject(t)) for (r in t) h(this[r]) ? this[r](t[r]) : this.attr(r, t[r]);
						return this
					}
					return (o = v.getElementById(r[2])) && (this[0] = o, this.length = 1),
					this
				}
				return e.nodeType ? (this[0] = e, this.length = 1, this) : h(e) ? void 0 !== n.ready ? n.ready(e) : e(w) : w.makeArray(e, this)
			}).prototype = w.fn,
			j = w(v);
			var I = /^(?:parents|prev(?:Until|All))/,
			L = {
				children: !0,
				contents: !0,
				next: !0,
				prev: !0
			};
			function q(e, t) {
				for (; (e = e[t]) && 1 !== e.nodeType;);
				return e
			}
			w.fn.extend({
				has: function(e) {
					var t = w(e, this),
					n = t.length;
					return this.filter((function() {
						for (var e = 0; e < n; e++) if (w.contains(this, t[e])) return ! 0
					}))
				},
				closest: function(e, t) {
					var n, r = 0,
					o = this.length,
					i = [],
					a = "string" != typeof e && w(e);
					if (!N.test(e)) for (; r < o; r++) for (n = this[r]; n && n !== t; n = n.parentNode) if (n.nodeType < 11 && (a ? a.index(n) > -1 : 1 === n.nodeType && w.find.matchesSelector(n, e))) {
						i.push(n);
						break
					}
					return this.pushStack(i.length > 1 ? w.uniqueSort(i) : i)
				},
				index: function(e) {
					return e ? "string" == typeof e ? s.call(w(e), this[0]) : s.call(this, e.jquery ? e[0] : e) : this[0] && this[0].parentNode ? this.first().prevAll().length: -1
				},
				add: function(e, t) {
					return this.pushStack(w.uniqueSort(w.merge(this.get(), w(e, t))))
				},
				addBack: function(e) {
					return this.add(null == e ? this.prevObject: this.prevObject.filter(e))
				}
			}),
			w.each({
				parent: function(e) {
					var t = e.parentNode;
					return t && 11 !== t.nodeType ? t: null
				},
				parents: function(e) {
					return C(e, "parentNode")
				},
				parentsUntil: function(e, t, n) {
					return C(e, "parentNode", n)
				},
				next: function(e) {
					return q(e, "nextSibling")
				},
				prev: function(e) {
					return q(e, "previousSibling")
				},
				nextAll: function(e) {
					return C(e, "nextSibling")
				},
				prevAll: function(e) {
					return C(e, "previousSibling")
				},
				nextUntil: function(e, t, n) {
					return C(e, "nextSibling", n)
				},
				prevUntil: function(e, t, n) {
					return C(e, "previousSibling", n)
				},
				siblings: function(e) {
					return D((e.parentNode || {}).firstChild, e)
				},
				children: function(e) {
					return D(e.firstChild)
				},
				contents: function(e) {
					return null != e.contentDocument && r(e.contentDocument) ? e.contentDocument: (E(e, "template") && (e = e.content || e), w.merge([], e.childNodes))
				}
			},
			(function(e, t) {
				w.fn[e] = function(n, r) {
					var o = w.map(this, t, n);
					return "Until" !== e.slice(-5) && (r = n),
					r && "string" == typeof r && (o = w.filter(r, o)),
					this.length > 1 && (L[e] || w.uniqueSort(o), I.test(e) && o.reverse()),
					this.pushStack(o)
				}
			}));
			var R = /[^\x20\t\r\n\f]+/g;
			function H(e) {
				return e
			}
			function F(e) {
				throw e
			}
			function P(e, t, n, r) {
				var o;
				try {
					e && h(o = e.promise) ? o.call(e).done(t).fail(n) : e && h(o = e.then) ? o.call(e, t, n) : t.apply(void 0, [e].slice(r))
				} catch(e) {
					n.apply(void 0, [e])
				}
			}
			w.Callbacks = function(e) {
				e = "string" == typeof e ?
				function(e) {
					var t = {};
					return w.each(e.match(R) || [], (function(e, n) {
						t[n] = !0
					})),
					t
				} (e) : w.extend({},
				e);
				var t, n, r, o, i = [],
				a = [],
				s = -1,
				u = function() {
					for (o = o || e.once, r = t = !0; a.length; s = -1) for (n = a.shift(); ++s < i.length;) ! 1 === i[s].apply(n[0], n[1]) && e.stopOnFalse && (s = i.length, n = !1);
					e.memory || (n = !1),
					t = !1,
					o && (i = n ? [] : "")
				},
				l = {
					add: function() {
						return i && (n && !t && (s = i.length - 1, a.push(n)),
						function t(n) {
							w.each(n, (function(n, r) {
								h(r) ? e.unique && l.has(r) || i.push(r) : r && r.length && "string" !== x(r) && t(r)
							}))
						} (arguments), n && !t && u()),
						this
					},
					remove: function() {
						return w.each(arguments, (function(e, t) {
							for (var n; (n = w.inArray(t, i, n)) > -1;) i.splice(n, 1),
							n <= s && s--
						})),
						this
					},
					has: function(e) {
						return e ? w.inArray(e, i) > -1 : i.length > 0
					},
					empty: function() {
						return i && (i = []),
						this
					},
					disable: function() {
						return o = a = [],
						i = n = "",
						this
					},
					disabled: function() {
						return ! i
					},
					lock: function() {
						return o = a = [],
						n || t || (i = n = ""),
						this
					},
					locked: function() {
						return !! o
					},
					fireWith: function(e, n) {
						return o || (n = [e, (n = n || []).slice ? n.slice() : n], a.push(n), t || u()),
						this
					},
					fire: function() {
						return l.fireWith(this, arguments),
						this
					},
					fired: function() {
						return !! r
					}
				};
				return l
			},
			w.extend({
				Deferred: function(t) {
					var n = [["notify", "progress", w.Callbacks("memory"), w.Callbacks("memory"), 2], ["resolve", "done", w.Callbacks("once memory"), w.Callbacks("once memory"), 0, "resolved"], ["reject", "fail", w.Callbacks("once memory"), w.Callbacks("once memory"), 1, "rejected"]],
					r = "pending",
					o = {
						state: function() {
							return r
						},
						always: function() {
							return i.done(arguments).fail(arguments),
							this
						},
						catch: function(e) {
							return o.then(null, e)
						},
						pipe: function() {
							var e = arguments;
							return w.Deferred((function(t) {
								w.each(n, (function(n, r) {
									var o = h(e[r[4]]) && e[r[4]];
									i[r[1]]((function() {
										var e = o && o.apply(this, arguments);
										e && h(e.promise) ? e.promise().progress(t.notify).done(t.resolve).fail(t.reject) : t[r[0] + "With"](this, o ? [e] : arguments)
									}))
								})),
								e = null
							})).promise()
						},
						then: function(t, r, o) {
							var i = 0;
							function a(t, n, r, o) {
								return function() {
									var s = this,
									u = arguments,
									l = function() {
										var e, l;
										if (! (t < i)) {
											if ((e = r.apply(s, u)) === n.promise()) throw new TypeError("Thenable self-resolution");
											l = e && ("object" == typeof e || "function" == typeof e) && e.then,
											h(l) ? o ? l.call(e, a(i, n, H, o), a(i, n, F, o)) : (i++, l.call(e, a(i, n, H, o), a(i, n, F, o), a(i, n, H, n.notifyWith))) : (r !== H && (s = void 0, u = [e]), (o || n.resolveWith)(s, u))
										}
									},
									c = o ? l: function() {
										try {
											l()
										} catch(e) {
											w.Deferred.exceptionHook && w.Deferred.exceptionHook(e, c.stackTrace),
											t + 1 >= i && (r !== F && (s = void 0, u = [e]), n.rejectWith(s, u))
										}
									};
									t ? c() : (w.Deferred.getStackHook && (c.stackTrace = w.Deferred.getStackHook()), e.setTimeout(c))
								}
							}
							return w.Deferred((function(e) {
								n[0][3].add(a(0, e, h(o) ? o: H, e.notifyWith)),
								n[1][3].add(a(0, e, h(t) ? t: H)),
								n[2][3].add(a(0, e, h(r) ? r: F))
							})).promise()
						},
						promise: function(e) {
							return null != e ? w.extend(e, o) : o
						}
					},
					i = {};
					return w.each(n, (function(e, t) {
						var a = t[2],
						s = t[5];
						o[t[1]] = a.add,
						s && a.add((function() {
							r = s
						}), n[3 - e][2].disable, n[3 - e][3].disable, n[0][2].lock, n[0][3].lock),
						a.add(t[3].fire),
						i[t[0]] = function() {
							return i[t[0] + "With"](this === i ? void 0 : this, arguments),
							this
						},
						i[t[0] + "With"] = a.fireWith
					})),
					o.promise(i),
					t && t.call(i, i),
					i
				},
				when: function(e) {
					var t = arguments.length,
					n = t,
					r = Array(n),
					i = o.call(arguments),
					a = w.Deferred(),
					s = function(e) {
						return function(n) {
							r[e] = this,
							i[e] = arguments.length > 1 ? o.call(arguments) : n,
							--t || a.resolveWith(r, i)
						}
					};
					if (t <= 1 && (P(e, a.done(s(n)).resolve, a.reject, !t), "pending" === a.state() || h(i[n] && i[n].then))) return a.then();
					for (; n--;) P(i[n], s(n), a.reject);
					return a.promise()
				}
			});
			var M = /^(Eval|Internal|Range|Reference|Syntax|Type|URI)Error$/;
			w.Deferred.exceptionHook = function(t, n) {
				e.console && e.console.warn && t && M.test(t.name) && e.console.warn("jQuery.Deferred exception: " + t.message, t.stack, n)
			},
			w.readyException = function(t) {
				e.setTimeout((function() {
					throw t
				}))
			};
			var B = w.Deferred();
			function $() {
				v.removeEventListener("DOMContentLoaded", $),
				e.removeEventListener("load", $),
				w.ready()
			}
			w.fn.ready = function(e) {
				return B.then(e).
				catch((function(e) {
					w.readyException(e)
				})),
				this
			},
			w.extend({
				isReady: !1,
				readyWait: 1,
				ready: function(e) { (!0 === e ? --w.readyWait: w.isReady) || (w.isReady = !0, !0 !== e && --w.readyWait > 0 || B.resolveWith(v, [w]))
				}
			}),
			w.ready.then = B.then,
			"complete" === v.readyState || "loading" !== v.readyState && !v.documentElement.doScroll ? e.setTimeout(w.ready) : (v.addEventListener("DOMContentLoaded", $), e.addEventListener("load", $));
			var W = function(e, t, n, r, o, i, a) {
				var s = 0,
				u = e.length,
				l = null == n;
				if ("object" === x(n)) for (s in o = !0, n) W(e, t, s, n[s], !0, i, a);
				else if (void 0 !== r && (o = !0, h(r) || (a = !0), l && (a ? (t.call(e, r), t = null) : (l = t, t = function(e, t, n) {
					return l.call(w(e), n)
				})), t)) for (; s < u; s++) t(e[s], n, a ? r: r.call(e[s], s, t(e[s], n)));
				return o ? e: l ? t.call(e) : u ? t(e[0], n) : i
			},
			_ = /^-ms-/,
			U = /-([a-z])/g;
			function J(e, t) {
				return t.toUpperCase()
			}
			function z(e) {
				return e.replace(_, "ms-").replace(U, J)
			}
			var X = function(e) {
				return 1 === e.nodeType || 9 === e.nodeType || !+e.nodeType
			};
			function G() {
				this.expando = w.expando + G.uid++
			}
			G.uid = 1,
			G.prototype = {
				cache: function(e) {
					var t = e[this.expando];
					return t || (t = {},
					X(e) && (e.nodeType ? e[this.expando] = t: Object.defineProperty(e, this.expando, {
						value: t,
						configurable: !0
					}))),
					t
				},
				set: function(e, t, n) {
					var r, o = this.cache(e);
					if ("string" == typeof t) o[z(t)] = n;
					else for (r in t) o[z(r)] = t[r];
					return o
				},
				get: function(e, t) {
					return void 0 === t ? this.cache(e) : e[this.expando] && e[this.expando][z(t)]
				},
				access: function(e, t, n) {
					return void 0 === t || t && "string" == typeof t && void 0 === n ? this.get(e, t) : (this.set(e, t, n), void 0 !== n ? n: t)
				},
				remove: function(e, t) {
					var n, r = e[this.expando];
					if (void 0 !== r) {
						if (void 0 !== t) {
							n = (t = Array.isArray(t) ? t.map(z) : (t = z(t)) in r ? [t] : t.match(R) || []).length;
							for (; n--;) delete r[t[n]]
						} (void 0 === t || w.isEmptyObject(r)) && (e.nodeType ? e[this.expando] = void 0 : delete e[this.expando])
					}
				},
				hasData: function(e) {
					var t = e[this.expando];
					return void 0 !== t && !w.isEmptyObject(t)
				}
			};
			var V = new G,
			Y = new G,
			Q = /^(?:\{[\w\W]*\}|\[[\w\W]*\])$/,
			K = /[A-Z]/g;
			function Z(e, t, n) {
				var r;
				if (void 0 === n && 1 === e.nodeType) if (r = "data-" + t.replace(K, "-$&").toLowerCase(), "string" == typeof(n = e.getAttribute(r))) {
					try {
						n = function(e) {
							return "true" === e || "false" !== e && ("null" === e ? null: e === +e + "" ? +e: Q.test(e) ? JSON.parse(e) : e)
						} (n)
					} catch(e) {}
					Y.set(e, t, n)
				} else n = void 0;
				return n
			}
			w.extend({
				hasData: function(e) {
					return Y.hasData(e) || V.hasData(e)
				},
				data: function(e, t, n) {
					return Y.access(e, t, n)
				},
				removeData: function(e, t) {
					Y.remove(e, t)
				},
				_data: function(e, t, n) {
					return V.access(e, t, n)
				},
				_removeData: function(e, t) {
					V.remove(e, t)
				}
			}),
			w.fn.extend({
				data: function(e, t) {
					var n, r, o, i = this[0],
					a = i && i.attributes;
					if (void 0 === e) {
						if (this.length && (o = Y.get(i), 1 === i.nodeType && !V.get(i, "hasDataAttrs"))) {
							for (n = a.length; n--;) a[n] && 0 === (r = a[n].name).indexOf("data-") && (r = z(r.slice(5)), Z(i, r, o[r]));
							V.set(i, "hasDataAttrs", !0)
						}
						return o
					}
					return "object" == typeof e ? this.each((function() {
						Y.set(this, e)
					})) : W(this, (function(t) {
						var n;
						if (i && void 0 === t) return void 0 !== (n = Y.get(i, e)) || void 0 !== (n = Z(i, e)) ? n: void 0;
						this.each((function() {
							Y.set(this, e, t)
						}))
					}), null, t, arguments.length > 1, null, !0)
				},
				removeData: function(e) {
					return this.each((function() {
						Y.remove(this, e)
					}))
				}
			}),
			w.extend({
				queue: function(e, t, n) {
					var r;
					if (e) return t = (t || "fx") + "queue",
					r = V.get(e, t),
					n && (!r || Array.isArray(n) ? r = V.access(e, t, w.makeArray(n)) : r.push(n)),
					r || []
				},
				dequeue: function(e, t) {
					t = t || "fx";
					var n = w.queue(e, t),
					r = n.length,
					o = n.shift(),
					i = w._queueHooks(e, t);
					"inprogress" === o && (o = n.shift(), r--),
					o && ("fx" === t && n.unshift("inprogress"), delete i.stop, o.call(e, (function() {
						w.dequeue(e, t)
					}), i)),
					!r && i && i.empty.fire()
				},
				_queueHooks: function(e, t) {
					var n = t + "queueHooks";
					return V.get(e, n) || V.access(e, n, {
						empty: w.Callbacks("once memory").add((function() {
							V.remove(e, [t + "queue", n])
						}))
					})
				}
			}),
			w.fn.extend({
				queue: function(e, t) {
					var n = 2;
					return "string" != typeof e && (t = e, e = "fx", n--),
					arguments.length < n ? w.queue(this[0], e) : void 0 === t ? this: this.each((function() {
						var n = w.queue(this, e, t);
						w._queueHooks(this, e),
						"fx" === e && "inprogress" !== n[0] && w.dequeue(this, e)
					}))
				},
				dequeue: function(e) {
					return this.each((function() {
						w.dequeue(this, e)
					}))
				},
				clearQueue: function(e) {
					return this.queue(e || "fx", [])
				},
				promise: function(e, t) {
					var n, r = 1,
					o = w.Deferred(),
					i = this,
					a = this.length,
					s = function() {--r || o.resolveWith(i, [i])
					};
					for ("string" != typeof e && (t = e, e = void 0), e = e || "fx"; a--;)(n = V.get(i[a], e + "queueHooks")) && n.empty && (r++, n.empty.add(s));
					return s(),
					o.promise(t)
				}
			});
			var ee = /[+-]?(?:\d*\.|)\d+(?:[eE][+-]?\d+|)/.source,
			te = new RegExp("^(?:([+-])=|)(" + ee + ")([a-z%]*)$", "i"),
			ne = ["Top", "Right", "Bottom", "Left"],
			re = v.documentElement,
			oe = function(e) {
				return w.contains(e.ownerDocument, e)
			},
			ie = {
				composed: !0
			};
			re.getRootNode && (oe = function(e) {
				return w.contains(e.ownerDocument, e) || e.getRootNode(ie) === e.ownerDocument
			});
			var ae = function(e, t) {
				return "none" === (e = t || e).style.display || "" === e.style.display && oe(e) && "none" === w.css(e, "display")
			};
			function se(e, t, n, r) {
				var o, i, a = 20,
				s = r ?
				function() {
					return r.cur()
				}: function() {
					return w.css(e, t, "")
				},
				u = s(),
				l = n && n[3] || (w.cssNumber[t] ? "": "px"),
				c = e.nodeType && (w.cssNumber[t] || "px" !== l && +u) && te.exec(w.css(e, t));
				if (c && c[3] !== l) {
					for (u /= 2, l = l || c[3], c = +u || 1; a--;) w.style(e, t, c + l),
					(1 - i) * (1 - (i = s() / u || .5)) <= 0 && (a = 0),
					c /= i;
					c *= 2,
					w.style(e, t, c + l),
					n = n || []
				}
				return n && (c = +c || +u || 0, o = n[1] ? c + (n[1] + 1) * n[2] : +n[2], r && (r.unit = l, r.start = c, r.end = o)),
				o
			}
			var ue = {};
			function le(e) {
				var t, n = e.ownerDocument,
				r = e.nodeName,
				o = ue[r];
				return o || (t = n.body.appendChild(n.createElement(r)), o = w.css(t, "display"), t.parentNode.removeChild(t), "none" === o && (o = "block"), ue[r] = o, o)
			}
			function ce(e, t) {
				for (var n, r, o = [], i = 0, a = e.length; i < a; i++)(r = e[i]).style && (n = r.style.display, t ? ("none" === n && (o[i] = V.get(r, "display") || null, o[i] || (r.style.display = "")), "" === r.style.display && ae(r) && (o[i] = le(r))) : "none" !== n && (o[i] = "none", V.set(r, "display", n)));
				for (i = 0; i < a; i++) null != o[i] && (e[i].style.display = o[i]);
				return e
			}
			w.fn.extend({
				show: function() {
					return ce(this, !0)
				},
				hide: function() {
					return ce(this)
				},
				toggle: function(e) {
					return "boolean" == typeof e ? e ? this.show() : this.hide() : this.each((function() {
						ae(this) ? w(this).show() : w(this).hide()
					}))
				}
			});
			var fe, pe, de = /^(?:checkbox|radio)$/i,
			he = /<([a-z][^\/\0>\x20\t\r\n\f]*)/i,
			ge = /^$|^module$|\/(?:java|ecma)script/i;
			fe = v.createDocumentFragment().appendChild(v.createElement("div")),
			(pe = v.createElement("input")).setAttribute("type", "radio"),
			pe.setAttribute("checked", "checked"),
			pe.setAttribute("name", "t"),
			fe.appendChild(pe),
			d.checkClone = fe.cloneNode(!0).cloneNode(!0).lastChild.checked,
			fe.innerHTML = "<textarea>x</textarea>",
			d.noCloneChecked = !!fe.cloneNode(!0).lastChild.defaultValue,
			fe.innerHTML = "<option></option>",
			d.option = !!fe.lastChild;
			var ve = {
				thead: [1, "<table>", "</table>"],
				col: [2, "<table><colgroup>", "</colgroup></table>"],
				tr: [2, "<table><tbody>", "</tbody></table>"],
				td: [3, "<table><tbody><tr>", "</tr></tbody></table>"],
				_default: [0, "", ""]
			};
			function ye(e, t) {
				var n;
				return n = void 0 !== e.getElementsByTagName ? e.getElementsByTagName(t || "*") : void 0 !== e.querySelectorAll ? e.querySelectorAll(t || "*") : [],
				void 0 === t || t && E(e, t) ? w.merge([e], n) : n
			}
			function me(e, t) {
				for (var n = 0,
				r = e.length; n < r; n++) V.set(e[n], "globalEval", !t || V.get(t[n], "globalEval"))
			}
			ve.tbody = ve.tfoot = ve.colgroup = ve.caption = ve.thead,
			ve.th = ve.td,
			d.option || (ve.optgroup = ve.option = [1, "<select multiple='multiple'>", "</select>"]);
			var xe = /<|&#?\w+;/;
			function be(e, t, n, r, o) {
				for (var i, a, s, u, l, c, f = t.createDocumentFragment(), p = [], d = 0, h = e.length; d < h; d++) if ((i = e[d]) || 0 === i) if ("object" === x(i)) w.merge(p, i.nodeType ? [i] : i);
				else if (xe.test(i)) {
					for (a = a || f.appendChild(t.createElement("div")), s = (he.exec(i) || ["", ""])[1].toLowerCase(), u = ve[s] || ve._default, a.innerHTML = u[1] + w.htmlPrefilter(i) + u[2], c = u[0]; c--;) a = a.lastChild;
					w.merge(p, a.childNodes),
					(a = f.firstChild).textContent = ""
				} else p.push(t.createTextNode(i));
				for (f.textContent = "", d = 0; i = p[d++];) if (r && w.inArray(i, r) > -1) o && o.push(i);
				else if (l = oe(i), a = ye(f.appendChild(i), "script"), l && me(a), n) for (c = 0; i = a[c++];) ge.test(i.type || "") && n.push(i);
				return f
			}
			var we = /^([^.]*)(?:\.(.+)|)/;
			function Te() {
				return ! 0
			}
			function Se() {
				return ! 1
			}
			function Ce(e, t) {
				return e ===
				function() {
					try {
						return v.activeElement
					} catch(e) {}
				} () == ("focus" === t)
			}
			function De(e, t, n, r, o, i) {
				var a, s;
				if ("object" == typeof t) {
					for (s in "string" != typeof n && (r = r || n, n = void 0), t) De(e, s, n, r, t[s], i);
					return e
				}
				if (null == r && null == o ? (o = n, r = n = void 0) : null == o && ("string" == typeof n ? (o = r, r = void 0) : (o = r, r = n, n = void 0)), !1 === o) o = Se;
				else if (!o) return e;
				return 1 === i && (a = o, o = function(e) {
					return w().off(e),
					a.apply(this, arguments)
				},
				o.guid = a.guid || (a.guid = w.guid++)),
				e.each((function() {
					w.event.add(this, t, o, r, n)
				}))
			}
			function Ne(e, t, n) {
				n ? (V.set(e, t, !1), w.event.add(e, t, {
					namespace: !1,
					handler: function(e) {
						var r, i, a = V.get(this, t);
						if (1 & e.isTrigger && this[t]) {
							if (a.length)(w.event.special[t] || {}).delegateType && e.stopPropagation();
							else if (a = o.call(arguments), V.set(this, t, a), r = n(this, t), this[t](), a !== (i = V.get(this, t)) || r ? V.set(this, t, !1) : i = {},
							a !== i) return e.stopImmediatePropagation(),
							e.preventDefault(),
							i && i.value
						} else a.length && (V.set(this, t, {
							value: w.event.trigger(w.extend(a[0], w.Event.prototype), a.slice(1), this)
						}), e.stopImmediatePropagation())
					}
				})) : void 0 === V.get(e, t) && w.event.add(e, t, Te)
			}
			w.event = {
				global: {},
				add: function(e, t, n, r, o) {
					var i, a, s, u, l, c, f, p, d, h, g, v = V.get(e);
					if (X(e)) for (n.handler && (n = (i = n).handler, o = i.selector), o && w.find.matchesSelector(re, o), n.guid || (n.guid = w.guid++), (u = v.events) || (u = v.events = Object.create(null)), (a = v.handle) || (a = v.handle = function(t) {
						return void 0 !== w && w.event.triggered !== t.type ? w.event.dispatch.apply(e, arguments) : void 0
					}), l = (t = (t || "").match(R) || [""]).length; l--;) d = g = (s = we.exec(t[l]) || [])[1],
					h = (s[2] || "").split(".").sort(),
					d && (f = w.event.special[d] || {},
					d = (o ? f.delegateType: f.bindType) || d, f = w.event.special[d] || {},
					c = w.extend({
						type: d,
						origType: g,
						data: r,
						handler: n,
						guid: n.guid,
						selector: o,
						needsContext: o && w.expr.match.needsContext.test(o),
						namespace: h.join(".")
					},
					i), (p = u[d]) || ((p = u[d] = []).delegateCount = 0, f.setup && !1 !== f.setup.call(e, r, h, a) || e.addEventListener && e.addEventListener(d, a)), f.add && (f.add.call(e, c), c.handler.guid || (c.handler.guid = n.guid)), o ? p.splice(p.delegateCount++, 0, c) : p.push(c), w.event.global[d] = !0)
				},
				remove: function(e, t, n, r, o) {
					var i, a, s, u, l, c, f, p, d, h, g, v = V.hasData(e) && V.get(e);
					if (v && (u = v.events)) {
						for (l = (t = (t || "").match(R) || [""]).length; l--;) if (d = g = (s = we.exec(t[l]) || [])[1], h = (s[2] || "").split(".").sort(), d) {
							for (f = w.event.special[d] || {},
							p = u[d = (r ? f.delegateType: f.bindType) || d] || [], s = s[2] && new RegExp("(^|\\.)" + h.join("\\.(?:.*\\.|)") + "(\\.|$)"), a = i = p.length; i--;) c = p[i],
							!o && g !== c.origType || n && n.guid !== c.guid || s && !s.test(c.namespace) || r && r !== c.selector && ("**" !== r || !c.selector) || (p.splice(i, 1), c.selector && p.delegateCount--, f.remove && f.remove.call(e, c));
							a && !p.length && (f.teardown && !1 !== f.teardown.call(e, h, v.handle) || w.removeEvent(e, d, v.handle), delete u[d])
						} else for (d in u) w.event.remove(e, d + t[l], n, r, !0);
						w.isEmptyObject(u) && V.remove(e, "handle events")
					}
				},
				dispatch: function(e) {
					var t, n, r, o, i, a, s = new Array(arguments.length),
					u = w.event.fix(e),
					l = (V.get(this, "events") || Object.create(null))[u.type] || [],
					c = w.event.special[u.type] || {};
					for (s[0] = u, t = 1; t < arguments.length; t++) s[t] = arguments[t];
					if (u.delegateTarget = this, !c.preDispatch || !1 !== c.preDispatch.call(this, u)) {
						for (a = w.event.handlers.call(this, u, l), t = 0; (o = a[t++]) && !u.isPropagationStopped();) for (u.currentTarget = o.elem, n = 0; (i = o.handlers[n++]) && !u.isImmediatePropagationStopped();) u.rnamespace && !1 !== i.namespace && !u.rnamespace.test(i.namespace) || (u.handleObj = i, u.data = i.data, void 0 !== (r = ((w.event.special[i.origType] || {}).handle || i.handler).apply(o.elem, s)) && !1 === (u.result = r) && (u.preventDefault(), u.stopPropagation()));
						return c.postDispatch && c.postDispatch.call(this, u),
						u.result
					}
				},
				handlers: function(e, t) {
					var n, r, o, i, a, s = [],
					u = t.delegateCount,
					l = e.target;
					if (u && l.nodeType && !("click" === e.type && e.button >= 1)) for (; l !== this; l = l.parentNode || this) if (1 === l.nodeType && ("click" !== e.type || !0 !== l.disabled)) {
						for (i = [], a = {},
						n = 0; n < u; n++) void 0 === a[o = (r = t[n]).selector + " "] && (a[o] = r.needsContext ? w(o, this).index(l) > -1 : w.find(o, this, null, [l]).length),
						a[o] && i.push(r);
						i.length && s.push({
							elem: l,
							handlers: i
						})
					}
					return l = this,
					u < t.length && s.push({
						elem: l,
						handlers: t.slice(u)
					}),
					s
				},
				addProp: function(e, t) {
					Object.defineProperty(w.Event.prototype, e, {
						enumerable: !0,
						configurable: !0,
						get: h(t) ?
						function() {
							if (this.originalEvent) return t(this.originalEvent)
						}: function() {
							if (this.originalEvent) return this.originalEvent[e]
						},
						set: function(t) {
							Object.defineProperty(this, e, {
								enumerable: !0,
								configurable: !0,
								writable: !0,
								value: t
							})
						}
					})
				},
				fix: function(e) {
					return e[w.expando] ? e: new w.Event(e)
				},
				special: {
					load: {
						noBubble: !0
					},
					click: {
						setup: function(e) {
							var t = this || e;
							return de.test(t.type) && t.click && E(t, "input") && Ne(t, "click", Te),
							!1
						},
						trigger: function(e) {
							var t = this || e;
							return de.test(t.type) && t.click && E(t, "input") && Ne(t, "click"),
							!0
						},
						_default: function(e) {
							var t = e.target;
							return de.test(t.type) && t.click && E(t, "input") && V.get(t, "click") || E(t, "a")
						}
					},
					beforeunload: {
						postDispatch: function(e) {
							void 0 !== e.result && e.originalEvent && (e.originalEvent.returnValue = e.result)
						}
					}
				}
			},
			w.removeEvent = function(e, t, n) {
				e.removeEventListener && e.removeEventListener(t, n)
			},
			w.Event = function(e, t) {
				if (! (this instanceof w.Event)) return new w.Event(e, t);
				e && e.type ? (this.originalEvent = e, this.type = e.type, this.isDefaultPrevented = e.defaultPrevented || void 0 === e.defaultPrevented && !1 === e.returnValue ? Te: Se, this.target = e.target && 3 === e.target.nodeType ? e.target.parentNode: e.target, this.currentTarget = e.currentTarget, this.relatedTarget = e.relatedTarget) : this.type = e,
				t && w.extend(this, t),
				this.timeStamp = e && e.timeStamp || Date.now(),
				this[w.expando] = !0
			},
			w.Event.prototype = {
				constructor: w.Event,
				isDefaultPrevented: Se,
				isPropagationStopped: Se,
				isImmediatePropagationStopped: Se,
				isSimulated: !1,
				preventDefault: function() {
					var e = this.originalEvent;
					this.isDefaultPrevented = Te,
					e && !this.isSimulated && e.preventDefault()
				},
				stopPropagation: function() {
					var e = this.originalEvent;
					this.isPropagationStopped = Te,
					e && !this.isSimulated && e.stopPropagation()
				},
				stopImmediatePropagation: function() {
					var e = this.originalEvent;
					this.isImmediatePropagationStopped = Te,
					e && !this.isSimulated && e.stopImmediatePropagation(),
					this.stopPropagation()
				}
			},
			w.each({
				altKey: !0,
				bubbles: !0,
				cancelable: !0,
				changedTouches: !0,
				ctrlKey: !0,
				detail: !0,
				eventPhase: !0,
				metaKey: !0,
				pageX: !0,
				pageY: !0,
				shiftKey: !0,
				view: !0,
				char: !0,
				code: !0,
				charCode: !0,
				key: !0,
				keyCode: !0,
				button: !0,
				buttons: !0,
				clientX: !0,
				clientY: !0,
				offsetX: !0,
				offsetY: !0,
				pointerId: !0,
				pointerType: !0,
				screenX: !0,
				screenY: !0,
				targetTouches: !0,
				toElement: !0,
				touches: !0,
				which: !0
			},
			w.event.addProp),
			w.each({
				focus: "focusin",
				blur: "focusout"
			},
			(function(e, t) {
				w.event.special[e] = {
					setup: function() {
						return Ne(this, e, Ce),
						!1
					},
					trigger: function() {
						return Ne(this, e),
						!0
					},
					_default: function() {
						return ! 0
					},
					delegateType: t
				}
			})),
			w.each({
				mouseenter: "mouseover",
				mouseleave: "mouseout",
				pointerenter: "pointerover",
				pointerleave: "pointerout"
			},
			(function(e, t) {
				w.event.special[e] = {
					delegateType: t,
					bindType: t,
					handle: function(e) {
						var n, r = this,
						o = e.relatedTarget,
						i = e.handleObj;
						return o && (o === r || w.contains(r, o)) || (e.type = i.origType, n = i.handler.apply(this, arguments), e.type = t),
						n
					}
				}
			})),
			w.fn.extend({
				on: function(e, t, n, r) {
					return De(this, e, t, n, r)
				},
				one: function(e, t, n, r) {
					return De(this, e, t, n, r, 1)
				},
				off: function(e, t, n) {
					var r, o;
					if (e && e.preventDefault && e.handleObj) return r = e.handleObj,
					w(e.delegateTarget).off(r.namespace ? r.origType + "." + r.namespace: r.origType, r.selector, r.handler),
					this;
					if ("object" == typeof e) {
						for (o in e) this.off(o, t, e[o]);
						return this
					}
					return ! 1 !== t && "function" != typeof t || (n = t, t = void 0),
					!1 === n && (n = Se),
					this.each((function() {
						w.event.remove(this, e, n, t)
					}))
				}
			});
			var Ee = /<script|<style|<link/i,
			Oe = /checked\s*(?:[^=]|=\s*.checked.)/i,
			ke = /^\s*<!(?:\[CDATA\[|--)|(?:\]\]|--)>\s*$/g;
			function je(e, t) {
				return E(e, "table") && E(11 !== t.nodeType ? t: t.firstChild, "tr") && w(e).children("tbody")[0] || e
			}
			function Ae(e) {
				return e.type = (null !== e.getAttribute("type")) + "/" + e.type,
				e
			}
			function Ie(e) {
				return "true/" === (e.type || "").slice(0, 5) ? e.type = e.type.slice(5) : e.removeAttribute("type"),
				e
			}
			function Le(e, t) {
				var n, r, o, i, a, s;
				if (1 === t.nodeType) {
					if (V.hasData(e) && (s = V.get(e).events)) for (o in V.remove(t, "handle events"), s) for (n = 0, r = s[o].length; n < r; n++) w.event.add(t, o, s[o][n]);
					Y.hasData(e) && (i = Y.access(e), a = w.extend({},
					i), Y.set(t, a))
				}
			}
			function qe(e, t) {
				var n = t.nodeName.toLowerCase();
				"input" === n && de.test(e.type) ? t.checked = e.checked: "input" !== n && "textarea" !== n || (t.defaultValue = e.defaultValue)
			}
			function Re(e, t, n, r) {
				t = i(t);
				var o, a, s, u, l, c, f = 0,
				p = e.length,
				g = p - 1,
				v = t[0],
				y = h(v);
				if (y || p > 1 && "string" == typeof v && !d.checkClone && Oe.test(v)) return e.each((function(o) {
					var i = e.eq(o);
					y && (t[0] = v.call(this, o, i.html())),
					Re(i, t, n, r)
				}));
				if (p && (a = (o = be(t, e[0].ownerDocument, !1, e, r)).firstChild, 1 === o.childNodes.length && (o = a), a || r)) {
					for (u = (s = w.map(ye(o, "script"), Ae)).length; f < p; f++) l = o,
					f !== g && (l = w.clone(l, !0, !0), u && w.merge(s, ye(l, "script"))),
					n.call(e[f], l, f);
					if (u) for (c = s[s.length - 1].ownerDocument, w.map(s, Ie), f = 0; f < u; f++) l = s[f],
					ge.test(l.type || "") && !V.access(l, "globalEval") && w.contains(c, l) && (l.src && "module" !== (l.type || "").toLowerCase() ? w._evalUrl && !l.noModule && w._evalUrl(l.src, {
						nonce: l.nonce || l.getAttribute("nonce")
					},
					c) : m(l.textContent.replace(ke, ""), l, c))
				}
				return e
			}
			function He(e, t, n) {
				for (var r, o = t ? w.filter(t, e) : e, i = 0; null != (r = o[i]); i++) n || 1 !== r.nodeType || w.cleanData(ye(r)),
				r.parentNode && (n && oe(r) && me(ye(r, "script")), r.parentNode.removeChild(r));
				return e
			}
			w.extend({
				htmlPrefilter: function(e) {
					return e
				},
				clone: function(e, t, n) {
					var r, o, i, a, s = e.cloneNode(!0),
					u = oe(e);
					if (! (d.noCloneChecked || 1 !== e.nodeType && 11 !== e.nodeType || w.isXMLDoc(e))) for (a = ye(s), r = 0, o = (i = ye(e)).length; r < o; r++) qe(i[r], a[r]);
					if (t) if (n) for (i = i || ye(e), a = a || ye(s), r = 0, o = i.length; r < o; r++) Le(i[r], a[r]);
					else Le(e, s);
					return (a = ye(s, "script")).length > 0 && me(a, !u && ye(e, "script")),
					s
				},
				cleanData: function(e) {
					for (var t, n, r, o = w.event.special,
					i = 0; void 0 !== (n = e[i]); i++) if (X(n)) {
						if (t = n[V.expando]) {
							if (t.events) for (r in t.events) o[r] ? w.event.remove(n, r) : w.removeEvent(n, r, t.handle);
							n[V.expando] = void 0
						}
						n[Y.expando] && (n[Y.expando] = void 0)
					}
				}
			}),
			w.fn.extend({
				detach: function(e) {
					return He(this, e, !0)
				},
				remove: function(e) {
					return He(this, e)
				},
				text: function(e) {
					return W(this, (function(e) {
						return void 0 === e ? w.text(this) : this.empty().each((function() {
							1 !== this.nodeType && 11 !== this.nodeType && 9 !== this.nodeType || (this.textContent = e)
						}))
					}), null, e, arguments.length)
				},
				append: function() {
					return Re(this, arguments, (function(e) {
						1 !== this.nodeType && 11 !== this.nodeType && 9 !== this.nodeType || je(this, e).appendChild(e)
					}))
				},
				prepend: function() {
					return Re(this, arguments, (function(e) {
						if (1 === this.nodeType || 11 === this.nodeType || 9 === this.nodeType) {
							var t = je(this, e);
							t.insertBefore(e, t.firstChild)
						}
					}))
				},
				before: function() {
					return Re(this, arguments, (function(e) {
						this.parentNode && this.parentNode.insertBefore(e, this)
					}))
				},
				after: function() {
					return Re(this, arguments, (function(e) {
						this.parentNode && this.parentNode.insertBefore(e, this.nextSibling)
					}))
				},
				empty: function() {
					for (var e, t = 0; null != (e = this[t]); t++) 1 === e.nodeType && (w.cleanData(ye(e, !1)), e.textContent = "");
					return this
				},
				clone: function(e, t) {
					return e = null != e && e,
					t = null == t ? e: t,
					this.map((function() {
						return w.clone(this, e, t)
					}))
				},
				html: function(e) {
					return W(this, (function(e) {
						var t = this[0] || {},
						n = 0,
						r = this.length;
						if (void 0 === e && 1 === t.nodeType) return t.innerHTML;
						if ("string" == typeof e && !Ee.test(e) && !ve[(he.exec(e) || ["", ""])[1].toLowerCase()]) {
							e = w.htmlPrefilter(e);
							try {
								for (; n < r; n++) 1 === (t = this[n] || {}).nodeType && (w.cleanData(ye(t, !1)), t.innerHTML = e);
								t = 0
							} catch(e) {}
						}
						t && this.empty().append(e)
					}), null, e, arguments.length)
				},
				replaceWith: function() {
					var e = [];
					return Re(this, arguments, (function(t) {
						var n = this.parentNode;
						w.inArray(this, e) < 0 && (w.cleanData(ye(this)), n && n.replaceChild(t, this))
					}), e)
				}
			}),
			w.each({
				appendTo: "append",
				prependTo: "prepend",
				insertBefore: "before",
				insertAfter: "after",
				replaceAll: "replaceWith"
			},
			(function(e, t) {
				w.fn[e] = function(e) {
					for (var n, r = [], o = w(e), i = o.length - 1, s = 0; s <= i; s++) n = s === i ? this: this.clone(!0),
					w(o[s])[t](n),
					a.apply(r, n.get());
					return this.pushStack(r)
				}
			}));
			var Fe = new RegExp("^(" + ee + ")(?!px)[a-z%]+$", "i"),
			Pe = function(t) {
				var n = t.ownerDocument.defaultView;
				return n && n.opener || (n = e),
				n.getComputedStyle(t)
			},
			Me = function(e, t, n) {
				var r, o, i = {};
				for (o in t) i[o] = e.style[o],
				e.style[o] = t[o];
				for (o in r = n.call(e), t) e.style[o] = i[o];
				return r
			},
			Be = new RegExp(ne.join("|"), "i");
			function $e(e, t, n) {
				var r, o, i, a, s = e.style;
				return (n = n || Pe(e)) && ("" !== (a = n.getPropertyValue(t) || n[t]) || oe(e) || (a = w.style(e, t)), !d.pixelBoxStyles() && Fe.test(a) && Be.test(t) && (r = s.width, o = s.minWidth, i = s.maxWidth, s.minWidth = s.maxWidth = s.width = a, a = n.width, s.width = r, s.minWidth = o, s.maxWidth = i)),
				void 0 !== a ? a + "": a
			}
			function We(e, t) {
				return {
					get: function() {
						if (!e()) return (this.get = t).apply(this, arguments);
						delete this.get
					}
				}
			} !
			function() {
				function t() {
					if (c) {
						l.style.cssText = "position:absolute;left:-11111px;width:60px;margin-top:1px;padding:0;border:0",
						c.style.cssText = "position:relative;display:block;box-sizing:border-box;overflow:scroll;margin:auto;border:1px;padding:1px;width:60%;top:1%",
						re.appendChild(l).appendChild(c);
						var t = e.getComputedStyle(c);
						r = "1%" !== t.top,
						u = 12 === n(t.marginLeft),
						c.style.right = "60%",
						a = 36 === n(t.right),
						o = 36 === n(t.width),
						c.style.position = "absolute",
						i = 12 === n(c.offsetWidth / 3),
						re.removeChild(l),
						c = null
					}
				}
				function n(e) {
					return Math.round(parseFloat(e))
				}
				var r, o, i, a, s, u, l = v.createElement("div"),
				c = v.createElement("div");
				c.style && (c.style.backgroundClip = "content-box", c.cloneNode(!0).style.backgroundClip = "", d.clearCloneStyle = "content-box" === c.style.backgroundClip, w.extend(d, {
					boxSizingReliable: function() {
						return t(),
						o
					},
					pixelBoxStyles: function() {
						return t(),
						a
					},
					pixelPosition: function() {
						return t(),
						r
					},
					reliableMarginLeft: function() {
						return t(),
						u
					},
					scrollboxSize: function() {
						return t(),
						i
					},
					reliableTrDimensions: function() {
						var t, n, r, o;
						return null == s && (t = v.createElement("table"), n = v.createElement("tr"), r = v.createElement("div"), t.style.cssText = "position:absolute;left:-11111px;border-collapse:separate", n.style.cssText = "border:1px solid", n.style.height = "1px", r.style.height = "9px", r.style.display = "block", re.appendChild(t).appendChild(n).appendChild(r), o = e.getComputedStyle(n), s = parseInt(o.height, 10) + parseInt(o.borderTopWidth, 10) + parseInt(o.borderBottomWidth, 10) === n.offsetHeight, re.removeChild(t)),
						s
					}
				}))
			} ();
			var _e = ["Webkit", "Moz", "ms"],
			Ue = v.createElement("div").style,
			Je = {};
			function ze(e) {
				var t = w.cssProps[e] || Je[e];
				return t || (e in Ue ? e: Je[e] = function(e) {
					for (var t = e[0].toUpperCase() + e.slice(1), n = _e.length; n--;) if ((e = _e[n] + t) in Ue) return e
				} (e) || e)
			}
			var Xe = /^(none|table(?!-c[ea]).+)/,
			Ge = /^--/,
			Ve = {
				position: "absolute",
				visibility: "hidden",
				display: "block"
			},
			Ye = {
				letterSpacing: "0",
				fontWeight: "400"
			};
			function Qe(e, t, n) {
				var r = te.exec(t);
				return r ? Math.max(0, r[2] - (n || 0)) + (r[3] || "px") : t
			}
			function Ke(e, t, n, r, o, i) {
				var a = "width" === t ? 1 : 0,
				s = 0,
				u = 0;
				if (n === (r ? "border": "content")) return 0;
				for (; a < 4; a += 2)"margin" === n && (u += w.css(e, n + ne[a], !0, o)),
				r ? ("content" === n && (u -= w.css(e, "padding" + ne[a], !0, o)), "margin" !== n && (u -= w.css(e, "border" + ne[a] + "Width", !0, o))) : (u += w.css(e, "padding" + ne[a], !0, o), "padding" !== n ? u += w.css(e, "border" + ne[a] + "Width", !0, o) : s += w.css(e, "border" + ne[a] + "Width", !0, o));
				return ! r && i >= 0 && (u += Math.max(0, Math.ceil(e["offset" + t[0].toUpperCase() + t.slice(1)] - i - u - s - .5)) || 0),
				u
			}
			function Ze(e, t, n) {
				var r = Pe(e),
				o = (!d.boxSizingReliable() || n) && "border-box" === w.css(e, "boxSizing", !1, r),
				i = o,
				a = $e(e, t, r),
				s = "offset" + t[0].toUpperCase() + t.slice(1);
				if (Fe.test(a)) {
					if (!n) return a;
					a = "auto"
				}
				return (!d.boxSizingReliable() && o || !d.reliableTrDimensions() && E(e, "tr") || "auto" === a || !parseFloat(a) && "inline" === w.css(e, "display", !1, r)) && e.getClientRects().length && (o = "border-box" === w.css(e, "boxSizing", !1, r), (i = s in e) && (a = e[s])),
				(a = parseFloat(a) || 0) + Ke(e, t, n || (o ? "border": "content"), i, r, a) + "px"
			}
			function et(e, t, n, r, o) {
				return new et.prototype.init(e, t, n, r, o)
			}
			w.extend({
				cssHooks: {
					opacity: {
						get: function(e, t) {
							if (t) {
								var n = $e(e, "opacity");
								return "" === n ? "1": n
							}
						}
					}
				},
				cssNumber: {
					animationIterationCount: !0,
					columnCount: !0,
					fillOpacity: !0,
					flexGrow: !0,
					flexShrink: !0,
					fontWeight: !0,
					gridArea: !0,
					gridColumn: !0,
					gridColumnEnd: !0,
					gridColumnStart: !0,
					gridRow: !0,
					gridRowEnd: !0,
					gridRowStart: !0,
					lineHeight: !0,
					opacity: !0,
					order: !0,
					orphans: !0,
					widows: !0,
					zIndex: !0,
					zoom: !0
				},
				cssProps: {},
				style: function(e, t, n, r) {
					if (e && 3 !== e.nodeType && 8 !== e.nodeType && e.style) {
						var o, i, a, s = z(t),
						u = Ge.test(t),
						l = e.style;
						if (u || (t = ze(s)), a = w.cssHooks[t] || w.cssHooks[s], void 0 === n) return a && "get" in a && void 0 !== (o = a.get(e, !1, r)) ? o: l[t];
						"string" === (i = typeof n) && (o = te.exec(n)) && o[1] && (n = se(e, t, o), i = "number"),
						null != n && n == n && ("number" !== i || u || (n += o && o[3] || (w.cssNumber[s] ? "": "px")), d.clearCloneStyle || "" !== n || 0 !== t.indexOf("background") || (l[t] = "inherit"), a && "set" in a && void 0 === (n = a.set(e, n, r)) || (u ? l.setProperty(t, n) : l[t] = n))
					}
				},
				css: function(e, t, n, r) {
					var o, i, a, s = z(t);
					return Ge.test(t) || (t = ze(s)),
					(a = w.cssHooks[t] || w.cssHooks[s]) && "get" in a && (o = a.get(e, !0, n)),
					void 0 === o && (o = $e(e, t, r)),
					"normal" === o && t in Ye && (o = Ye[t]),
					"" === n || n ? (i = parseFloat(o), !0 === n || isFinite(i) ? i || 0 : o) : o
				}
			}),
			w.each(["height", "width"], (function(e, t) {
				w.cssHooks[t] = {
					get: function(e, n, r) {
						if (n) return ! Xe.test(w.css(e, "display")) || e.getClientRects().length && e.getBoundingClientRect().width ? Ze(e, t, r) : Me(e, Ve, (function() {
							return Ze(e, t, r)
						}))
					},
					set: function(e, n, r) {
						var o, i = Pe(e),
						a = !d.scrollboxSize() && "absolute" === i.position,
						s = (a || r) && "border-box" === w.css(e, "boxSizing", !1, i),
						u = r ? Ke(e, t, r, s, i) : 0;
						return s && a && (u -= Math.ceil(e["offset" + t[0].toUpperCase() + t.slice(1)] - parseFloat(i[t]) - Ke(e, t, "border", !1, i) - .5)),
						u && (o = te.exec(n)) && "px" !== (o[3] || "px") && (e.style[t] = n, n = w.css(e, t)),
						Qe(0, n, u)
					}
				}
			})),
			w.cssHooks.marginLeft = We(d.reliableMarginLeft, (function(e, t) {
				if (t) return (parseFloat($e(e, "marginLeft")) || e.getBoundingClientRect().left - Me(e, {
					marginLeft: 0
				},
				(function() {
					return e.getBoundingClientRect().left
				}))) + "px"
			})),
			w.each({
				margin: "",
				padding: "",
				border: "Width"
			},
			(function(e, t) {
				w.cssHooks[e + t] = {
					expand: function(n) {
						for (var r = 0,
						o = {},
						i = "string" == typeof n ? n.split(" ") : [n]; r < 4; r++) o[e + ne[r] + t] = i[r] || i[r - 2] || i[0];
						return o
					}
				},
				"margin" !== e && (w.cssHooks[e + t].set = Qe)
			})),
			w.fn.extend({
				css: function(e, t) {
					return W(this, (function(e, t, n) {
						var r, o, i = {},
						a = 0;
						if (Array.isArray(t)) {
							for (r = Pe(e), o = t.length; a < o; a++) i[t[a]] = w.css(e, t[a], !1, r);
							return i
						}
						return void 0 !== n ? w.style(e, t, n) : w.css(e, t)
					}), e, t, arguments.length > 1)
				}
			}),
			w.Tween = et,
			et.prototype = {
				constructor: et,
				init: function(e, t, n, r, o, i) {
					this.elem = e,
					this.prop = n,
					this.easing = o || w.easing._default,
					this.options = t,
					this.start = this.now = this.cur(),
					this.end = r,
					this.unit = i || (w.cssNumber[n] ? "": "px")
				},
				cur: function() {
					var e = et.propHooks[this.prop];
					return e && e.get ? e.get(this) : et.propHooks._default.get(this)
				},
				run: function(e) {
					var t, n = et.propHooks[this.prop];
					return this.options.duration ? this.pos = t = w.easing[this.easing](e, this.options.duration * e, 0, 1, this.options.duration) : this.pos = t = e,
					this.now = (this.end - this.start) * t + this.start,
					this.options.step && this.options.step.call(this.elem, this.now, this),
					n && n.set ? n.set(this) : et.propHooks._default.set(this),
					this
				}
			},
			et.prototype.init.prototype = et.prototype,
			et.propHooks = {
				_default: {
					get: function(e) {
						var t;
						return 1 !== e.elem.nodeType || null != e.elem[e.prop] && null == e.elem.style[e.prop] ? e.elem[e.prop] : (t = w.css(e.elem, e.prop, "")) && "auto" !== t ? t: 0
					},
					set: function(e) {
						w.fx.step[e.prop] ? w.fx.step[e.prop](e) : 1 !== e.elem.nodeType || !w.cssHooks[e.prop] && null == e.elem.style[ze(e.prop)] ? e.elem[e.prop] = e.now: w.style(e.elem, e.prop, e.now + e.unit)
					}
				}
			},
			et.propHooks.scrollTop = et.propHooks.scrollLeft = {
				set: function(e) {
					e.elem.nodeType && e.elem.parentNode && (e.elem[e.prop] = e.now)
				}
			},
			w.easing = {
				linear: function(e) {
					return e
				},
				swing: function(e) {
					return.5 - Math.cos(e * Math.PI) / 2
				},
				_default: "swing"
			},
			w.fx = et.prototype.init,
			w.fx.step = {};
			var tt, nt, rt = /^(?:toggle|show|hide)$/,
			ot = /queueHooks$/;
			function it() {
				nt && (!1 === v.hidden && e.requestAnimationFrame ? e.requestAnimationFrame(it) : e.setTimeout(it, w.fx.interval), w.fx.tick())
			}
			function at() {
				return e.setTimeout((function() {
					tt = void 0
				})),
				tt = Date.now()
			}
			function st(e, t) {
				var n, r = 0,
				o = {
					height: e
				};
				for (t = t ? 1 : 0; r < 4; r += 2 - t) o["margin" + (n = ne[r])] = o["padding" + n] = e;
				return t && (o.opacity = o.width = e),
				o
			}
			function ut(e, t, n) {
				for (var r, o = (lt.tweeners[t] || []).concat(lt.tweeners["*"]), i = 0, a = o.length; i < a; i++) if (r = o[i].call(n, t, e)) return r
			}
			function lt(e, t, n) {
				var r, o, i = 0,
				a = lt.prefilters.length,
				s = w.Deferred().always((function() {
					delete u.elem
				})),
				u = function() {
					if (o) return ! 1;
					for (var t = tt || at(), n = Math.max(0, l.startTime + l.duration - t), r = 1 - (n / l.duration || 0), i = 0, a = l.tweens.length; i < a; i++) l.tweens[i].run(r);
					return s.notifyWith(e, [l, r, n]),
					r < 1 && a ? n: (a || s.notifyWith(e, [l, 1, 0]), s.resolveWith(e, [l]), !1)
				},
				l = s.promise({
					elem: e,
					props: w.extend({},
					t),
					opts: w.extend(!0, {
						specialEasing: {},
						easing: w.easing._default
					},
					n),
					originalProperties: t,
					originalOptions: n,
					startTime: tt || at(),
					duration: n.duration,
					tweens: [],
					createTween: function(t, n) {
						var r = w.Tween(e, l.opts, t, n, l.opts.specialEasing[t] || l.opts.easing);
						return l.tweens.push(r),
						r
					},
					stop: function(t) {
						var n = 0,
						r = t ? l.tweens.length: 0;
						if (o) return this;
						for (o = !0; n < r; n++) l.tweens[n].run(1);
						return t ? (s.notifyWith(e, [l, 1, 0]), s.resolveWith(e, [l, t])) : s.rejectWith(e, [l, t]),
						this
					}
				}),
				c = l.props;
				for (!
				function(e, t) {
					var n, r, o, i, a;
					for (n in e) if (o = t[r = z(n)], i = e[n], Array.isArray(i) && (o = i[1], i = e[n] = i[0]), n !== r && (e[r] = i, delete e[n]), (a = w.cssHooks[r]) && "expand" in a) for (n in i = a.expand(i), delete e[r], i) n in e || (e[n] = i[n], t[n] = o);
					else t[r] = o
				} (c, l.opts.specialEasing); i < a; i++) if (r = lt.prefilters[i].call(l, e, c, l.opts)) return h(r.stop) && (w._queueHooks(l.elem, l.opts.queue).stop = r.stop.bind(r)),
				r;
				return w.map(c, ut, l),
				h(l.opts.start) && l.opts.start.call(e, l),
				l.progress(l.opts.progress).done(l.opts.done, l.opts.complete).fail(l.opts.fail).always(l.opts.always),
				w.fx.timer(w.extend(u, {
					elem: e,
					anim: l,
					queue: l.opts.queue
				})),
				l
			}
			w.Animation = w.extend(lt, {
				tweeners: {
					"*": [function(e, t) {
						var n = this.createTween(e, t);
						return se(n.elem, e, te.exec(t), n),
						n
					}]
				},
				tweener: function(e, t) {
					h(e) ? (t = e, e = ["*"]) : e = e.match(R);
					for (var n, r = 0,
					o = e.length; r < o; r++) n = e[r],
					lt.tweeners[n] = lt.tweeners[n] || [],
					lt.tweeners[n].unshift(t)
				},
				prefilters: [function(e, t, n) {
					var r, o, i, a, s, u, l, c, f = "width" in t || "height" in t,
					p = this,
					d = {},
					h = e.style,
					g = e.nodeType && ae(e),
					v = V.get(e, "fxshow");
					for (r in n.queue || (null == (a = w._queueHooks(e, "fx")).unqueued && (a.unqueued = 0, s = a.empty.fire, a.empty.fire = function() {
						a.unqueued || s()
					}), a.unqueued++, p.always((function() {
						p.always((function() {
							a.unqueued--,
							w.queue(e, "fx").length || a.empty.fire()
						}))
					}))), t) if (o = t[r], rt.test(o)) {
						if (delete t[r], i = i || "toggle" === o, o === (g ? "hide": "show")) {
							if ("show" !== o || !v || void 0 === v[r]) continue;
							g = !0
						}
						d[r] = v && v[r] || w.style(e, r)
					}
					if ((u = !w.isEmptyObject(t)) || !w.isEmptyObject(d)) for (r in f && 1 === e.nodeType && (n.overflow = [h.overflow, h.overflowX, h.overflowY], null == (l = v && v.display) && (l = V.get(e, "display")), "none" === (c = w.css(e, "display")) && (l ? c = l: (ce([e], !0), l = e.style.display || l, c = w.css(e, "display"), ce([e]))), ("inline" === c || "inline-block" === c && null != l) && "none" === w.css(e, "float") && (u || (p.done((function() {
						h.display = l
					})), null == l && (c = h.display, l = "none" === c ? "": c)), h.display = "inline-block")), n.overflow && (h.overflow = "hidden", p.always((function() {
						h.overflow = n.overflow[0],
						h.overflowX = n.overflow[1],
						h.overflowY = n.overflow[2]
					}))), u = !1, d) u || (v ? "hidden" in v && (g = v.hidden) : v = V.access(e, "fxshow", {
						display: l
					}), i && (v.hidden = !g), g && ce([e], !0), p.done((function() {
						for (r in g || ce([e]), V.remove(e, "fxshow"), d) w.style(e, r, d[r])
					}))),
					u = ut(g ? v[r] : 0, r, p),
					r in v || (v[r] = u.start, g && (u.end = u.start, u.start = 0))
				}],
				prefilter: function(e, t) {
					t ? lt.prefilters.unshift(e) : lt.prefilters.push(e)
				}
			}),
			w.speed = function(e, t, n) {
				var r = e && "object" == typeof e ? w.extend({},
				e) : {
					complete: n || !n && t || h(e) && e,
					duration: e,
					easing: n && t || t && !h(t) && t
				};
				return w.fx.off ? r.duration = 0 : "number" != typeof r.duration && (r.duration in w.fx.speeds ? r.duration = w.fx.speeds[r.duration] : r.duration = w.fx.speeds._default),
				null != r.queue && !0 !== r.queue || (r.queue = "fx"),
				r.old = r.complete,
				r.complete = function() {
					h(r.old) && r.old.call(this),
					r.queue && w.dequeue(this, r.queue)
				},
				r
			},
			w.fn.extend({
				fadeTo: function(e, t, n, r) {
					return this.filter(ae).css("opacity", 0).show().end().animate({
						opacity: t
					},
					e, n, r)
				},
				animate: function(e, t, n, r) {
					var o = w.isEmptyObject(e),
					i = w.speed(t, n, r),
					a = function() {
						var t = lt(this, w.extend({},
						e), i); (o || V.get(this, "finish")) && t.stop(!0)
					};
					return a.finish = a,
					o || !1 === i.queue ? this.each(a) : this.queue(i.queue, a)
				},
				stop: function(e, t, n) {
					var r = function(e) {
						var t = e.stop;
						delete e.stop,
						t(n)
					};
					return "string" != typeof e && (n = t, t = e, e = void 0),
					t && this.queue(e || "fx", []),
					this.each((function() {
						var t = !0,
						o = null != e && e + "queueHooks",
						i = w.timers,
						a = V.get(this);
						if (o) a[o] && a[o].stop && r(a[o]);
						else for (o in a) a[o] && a[o].stop && ot.test(o) && r(a[o]);
						for (o = i.length; o--;) i[o].elem !== this || null != e && i[o].queue !== e || (i[o].anim.stop(n), t = !1, i.splice(o, 1)); ! t && n || w.dequeue(this, e)
					}))
				},
				finish: function(e) {
					return ! 1 !== e && (e = e || "fx"),
					this.each((function() {
						var t, n = V.get(this),
						r = n[e + "queue"],
						o = n[e + "queueHooks"],
						i = w.timers,
						a = r ? r.length: 0;
						for (n.finish = !0, w.queue(this, e, []), o && o.stop && o.stop.call(this, !0), t = i.length; t--;) i[t].elem === this && i[t].queue === e && (i[t].anim.stop(!0), i.splice(t, 1));
						for (t = 0; t < a; t++) r[t] && r[t].finish && r[t].finish.call(this);
						delete n.finish
					}))
				}
			}),
			w.each(["toggle", "show", "hide"], (function(e, t) {
				var n = w.fn[t];
				w.fn[t] = function(e, r, o) {
					return null == e || "boolean" == typeof e ? n.apply(this, arguments) : this.animate(st(t, !0), e, r, o)
				}
			})),
			w.each({
				slideDown: st("show"),
				slideUp: st("hide"),
				slideToggle: st("toggle"),
				fadeIn: {
					opacity: "show"
				},
				fadeOut: {
					opacity: "hide"
				},
				fadeToggle: {
					opacity: "toggle"
				}
			},
			(function(e, t) {
				w.fn[e] = function(e, n, r) {
					return this.animate(t, e, n, r)
				}
			})),
			w.timers = [],
			w.fx.tick = function() {
				var e, t = 0,
				n = w.timers;
				for (tt = Date.now(); t < n.length; t++)(e = n[t])() || n[t] !== e || n.splice(t--, 1);
				n.length || w.fx.stop(),
				tt = void 0
			},
			w.fx.timer = function(e) {
				w.timers.push(e),
				w.fx.start()
			},
			w.fx.interval = 13,
			w.fx.start = function() {
				nt || (nt = !0, it())
			},
			w.fx.stop = function() {
				nt = null
			},
			w.fx.speeds = {
				slow: 600,
				fast: 200,
				_default: 400
			},
			w.fn.delay = function(t, n) {
				return t = w.fx && w.fx.speeds[t] || t,
				n = n || "fx",
				this.queue(n, (function(n, r) {
					var o = e.setTimeout(n, t);
					r.stop = function() {
						e.clearTimeout(o)
					}
				}))
			},
			function() {
				var e = v.createElement("input"),
				t = v.createElement("select").appendChild(v.createElement("option"));
				e.type = "checkbox",
				d.checkOn = "" !== e.value,
				d.optSelected = t.selected,
				(e = v.createElement("input")).value = "t",
				e.type = "radio",
				d.radioValue = "t" === e.value
			} ();
			var ct, ft = w.expr.attrHandle;
			w.fn.extend({
				attr: function(e, t) {
					return W(this, w.attr, e, t, arguments.length > 1)
				},
				removeAttr: function(e) {
					return this.each((function() {
						w.removeAttr(this, e)
					}))
				}
			}),
			w.extend({
				attr: function(e, t, n) {
					var r, o, i = e.nodeType;
					if (3 !== i && 8 !== i && 2 !== i) return void 0 === e.getAttribute ? w.prop(e, t, n) : (1 === i && w.isXMLDoc(e) || (o = w.attrHooks[t.toLowerCase()] || (w.expr.match.bool.test(t) ? ct: void 0)), void 0 !== n ? null === n ? void w.removeAttr(e, t) : o && "set" in o && void 0 !== (r = o.set(e, n, t)) ? r: (e.setAttribute(t, n + ""), n) : o && "get" in o && null !== (r = o.get(e, t)) ? r: null == (r = w.find.attr(e, t)) ? void 0 : r)
				},
				attrHooks: {
					type: {
						set: function(e, t) {
							if (!d.radioValue && "radio" === t && E(e, "input")) {
								var n = e.value;
								return e.setAttribute("type", t),
								n && (e.value = n),
								t
							}
						}
					}
				},
				removeAttr: function(e, t) {
					var n, r = 0,
					o = t && t.match(R);
					if (o && 1 === e.nodeType) for (; n = o[r++];) e.removeAttribute(n)
				}
			}),
			ct = {
				set: function(e, t, n) {
					return ! 1 === t ? w.removeAttr(e, n) : e.setAttribute(n, n),
					n
				}
			},
			w.each(w.expr.match.bool.source.match(/\w+/g), (function(e, t) {
				var n = ft[t] || w.find.attr;
				ft[t] = function(e, t, r) {
					var o, i, a = t.toLowerCase();
					return r || (i = ft[a], ft[a] = o, o = null != n(e, t, r) ? a: null, ft[a] = i),
					o
				}
			}));
			var pt = /^(?:input|select|textarea|button)$/i,
			dt = /^(?:a|area)$/i;
			function ht(e) {
				return (e.match(R) || []).join(" ")
			}
			function gt(e) {
				return e.getAttribute && e.getAttribute("class") || ""
			}
			function vt(e) {
				return Array.isArray(e) ? e: "string" == typeof e && e.match(R) || []
			}
			w.fn.extend({
				prop: function(e, t) {
					return W(this, w.prop, e, t, arguments.length > 1)
				},
				removeProp: function(e) {
					return this.each((function() {
						delete this[w.propFix[e] || e]
					}))
				}
			}),
			w.extend({
				prop: function(e, t, n) {
					var r, o, i = e.nodeType;
					if (3 !== i && 8 !== i && 2 !== i) return 1 === i && w.isXMLDoc(e) || (t = w.propFix[t] || t, o = w.propHooks[t]),
					void 0 !== n ? o && "set" in o && void 0 !== (r = o.set(e, n, t)) ? r: e[t] = n: o && "get" in o && null !== (r = o.get(e, t)) ? r: e[t]
				},
				propHooks: {
					tabIndex: {
						get: function(e) {
							var t = w.find.attr(e, "tabindex");
							return t ? parseInt(t, 10) : pt.test(e.nodeName) || dt.test(e.nodeName) && e.href ? 0 : -1
						}
					}
				},
				propFix: {
					for: "htmlFor",
					class: "className"
				}
			}),
			d.optSelected || (w.propHooks.selected = {
				get: function(e) {
					var t = e.parentNode;
					return t && t.parentNode && t.parentNode.selectedIndex,
					null
				},
				set: function(e) {
					var t = e.parentNode;
					t && (t.selectedIndex, t.parentNode && t.parentNode.selectedIndex)
				}
			}),
			w.each(["tabIndex", "readOnly", "maxLength", "cellSpacing", "cellPadding", "rowSpan", "colSpan", "useMap", "frameBorder", "contentEditable"], (function() {
				w.propFix[this.toLowerCase()] = this
			})),
			w.fn.extend({
				addClass: function(e) {
					var t, n, r, o, i, a, s, u = 0;
					if (h(e)) return this.each((function(t) {
						w(this).addClass(e.call(this, t, gt(this)))
					}));
					if ((t = vt(e)).length) for (; n = this[u++];) if (o = gt(n), r = 1 === n.nodeType && " " + ht(o) + " ") {
						for (a = 0; i = t[a++];) r.indexOf(" " + i + " ") < 0 && (r += i + " ");
						o !== (s = ht(r)) && n.setAttribute("class", s)
					}
					return this
				},
				removeClass: function(e) {
					var t, n, r, o, i, a, s, u = 0;
					if (h(e)) return this.each((function(t) {
						w(this).removeClass(e.call(this, t, gt(this)))
					}));
					if (!arguments.length) return this.attr("class", "");
					if ((t = vt(e)).length) for (; n = this[u++];) if (o = gt(n), r = 1 === n.nodeType && " " + ht(o) + " ") {
						for (a = 0; i = t[a++];) for (; r.indexOf(" " + i + " ") > -1;) r = r.replace(" " + i + " ", " ");
						o !== (s = ht(r)) && n.setAttribute("class", s)
					}
					return this
				},
				toggleClass: function(e, t) {
					var n = typeof e,
					r = "string" === n || Array.isArray(e);
					return "boolean" == typeof t && r ? t ? this.addClass(e) : this.removeClass(e) : h(e) ? this.each((function(n) {
						w(this).toggleClass(e.call(this, n, gt(this), t), t)
					})) : this.each((function() {
						var t, o, i, a;
						if (r) for (o = 0, i = w(this), a = vt(e); t = a[o++];) i.hasClass(t) ? i.removeClass(t) : i.addClass(t);
						else void 0 !== e && "boolean" !== n || ((t = gt(this)) && V.set(this, "__className__", t), this.setAttribute && this.setAttribute("class", t || !1 === e ? "": V.get(this, "__className__") || ""))
					}))
				},
				hasClass: function(e) {
					var t, n, r = 0;
					for (t = " " + e + " "; n = this[r++];) if (1 === n.nodeType && (" " + ht(gt(n)) + " ").indexOf(t) > -1) return ! 0;
					return ! 1
				}
			});
			var yt = /\r/g;
			w.fn.extend({
				val: function(e) {
					var t, n, r, o = this[0];
					return arguments.length ? (r = h(e), this.each((function(n) {
						var o;
						1 === this.nodeType && (null == (o = r ? e.call(this, n, w(this).val()) : e) ? o = "": "number" == typeof o ? o += "": Array.isArray(o) && (o = w.map(o, (function(e) {
							return null == e ? "": e + ""
						}))), (t = w.valHooks[this.type] || w.valHooks[this.nodeName.toLowerCase()]) && "set" in t && void 0 !== t.set(this, o, "value") || (this.value = o))
					}))) : o ? (t = w.valHooks[o.type] || w.valHooks[o.nodeName.toLowerCase()]) && "get" in t && void 0 !== (n = t.get(o, "value")) ? n: "string" == typeof(n = o.value) ? n.replace(yt, "") : null == n ? "": n: void 0
				}
			}),
			w.extend({
				valHooks: {
					option: {
						get: function(e) {
							var t = w.find.attr(e, "value");
							return null != t ? t: ht(w.text(e))
						}
					},
					select: {
						get: function(e) {
							var t, n, r, o = e.options,
							i = e.selectedIndex,
							a = "select-one" === e.type,
							s = a ? null: [],
							u = a ? i + 1 : o.length;
							for (r = i < 0 ? u: a ? i: 0; r < u; r++) if (((n = o[r]).selected || r === i) && !n.disabled && (!n.parentNode.disabled || !E(n.parentNode, "optgroup"))) {
								if (t = w(n).val(), a) return t;
								s.push(t)
							}
							return s
						},
						set: function(e, t) {
							for (var n, r, o = e.options,
							i = w.makeArray(t), a = o.length; a--;)((r = o[a]).selected = w.inArray(w.valHooks.option.get(r), i) > -1) && (n = !0);
							return n || (e.selectedIndex = -1),
							i
						}
					}
				}
			}),
			w.each(["radio", "checkbox"], (function() {
				w.valHooks[this] = {
					set: function(e, t) {
						if (Array.isArray(t)) return e.checked = w.inArray(w(e).val(), t) > -1
					}
				},
				d.checkOn || (w.valHooks[this].get = function(e) {
					return null === e.getAttribute("value") ? "on": e.value
				})
			})),
			d.focusin = "onfocusin" in e;
			var mt = /^(?:focusinfocus|focusoutblur)$/,
			xt = function(e) {
				e.stopPropagation()
			};
			w.extend(w.event, {
				trigger: function(t, n, r, o) {
					var i, a, s, u, l, f, p, d, y = [r || v],
					m = c.call(t, "type") ? t.type: t,
					x = c.call(t, "namespace") ? t.namespace.split(".") : [];
					if (a = d = s = r = r || v, 3 !== r.nodeType && 8 !== r.nodeType && !mt.test(m + w.event.triggered) && (m.indexOf(".") > -1 && (x = m.split("."), m = x.shift(), x.sort()), l = m.indexOf(":") < 0 && "on" + m, (t = t[w.expando] ? t: new w.Event(m, "object" == typeof t && t)).isTrigger = o ? 2 : 3, t.namespace = x.join("."), t.rnamespace = t.namespace ? new RegExp("(^|\\.)" + x.join("\\.(?:.*\\.|)") + "(\\.|$)") : null, t.result = void 0, t.target || (t.target = r), n = null == n ? [t] : w.makeArray(n, [t]), p = w.event.special[m] || {},
					o || !p.trigger || !1 !== p.trigger.apply(r, n))) {
						if (!o && !p.noBubble && !g(r)) {
							for (u = p.delegateType || m, mt.test(u + m) || (a = a.parentNode); a; a = a.parentNode) y.push(a),
							s = a;
							s === (r.ownerDocument || v) && y.push(s.defaultView || s.parentWindow || e)
						}
						for (i = 0; (a = y[i++]) && !t.isPropagationStopped();) d = a,
						t.type = i > 1 ? u: p.bindType || m,
						(f = (V.get(a, "events") || Object.create(null))[t.type] && V.get(a, "handle")) && f.apply(a, n),
						(f = l && a[l]) && f.apply && X(a) && (t.result = f.apply(a, n), !1 === t.result && t.preventDefault());
						return t.type = m,
						o || t.isDefaultPrevented() || p._default && !1 !== p._default.apply(y.pop(), n) || !X(r) || l && h(r[m]) && !g(r) && ((s = r[l]) && (r[l] = null), w.event.triggered = m, t.isPropagationStopped() && d.addEventListener(m, xt), r[m](), t.isPropagationStopped() && d.removeEventListener(m, xt), w.event.triggered = void 0, s && (r[l] = s)),
						t.result
					}
				},
				simulate: function(e, t, n) {
					var r = w.extend(new w.Event, n, {
						type: e,
						isSimulated: !0
					});
					w.event.trigger(r, null, t)
				}
			}),
			w.fn.extend({
				trigger: function(e, t) {
					return this.each((function() {
						w.event.trigger(e, t, this)
					}))
				},
				triggerHandler: function(e, t) {
					var n = this[0];
					if (n) return w.event.trigger(e, t, n, !0)
				}
			}),
			d.focusin || w.each({
				focus: "focusin",
				blur: "focusout"
			},
			(function(e, t) {
				var n = function(e) {
					w.event.simulate(t, e.target, w.event.fix(e))
				};
				w.event.special[t] = {
					setup: function() {
						var r = this.ownerDocument || this.document || this,
						o = V.access(r, t);
						o || r.addEventListener(e, n, !0),
						V.access(r, t, (o || 0) + 1)
					},
					teardown: function() {
						var r = this.ownerDocument || this.document || this,
						o = V.access(r, t) - 1;
						o ? V.access(r, t, o) : (r.removeEventListener(e, n, !0), V.remove(r, t))
					}
				}
			}));
			var bt = e.location,
			wt = {
				guid: Date.now()
			},
			Tt = /\?/;
			w.parseXML = function(t) {
				var n, r;
				if (!t || "string" != typeof t) return null;
				try {
					n = (new e.DOMParser).parseFromString(t, "text/xml")
				} catch(e) {}
				return r = n && n.getElementsByTagName("parsererror")[0],
				n && !r || w.error("Invalid XML: " + (r ? w.map(r.childNodes, (function(e) {
					return e.textContent
				})).join("\n") : t)),
				n
			};
			var St = /\[\]$/,
			Ct = /\r?\n/g,
			Dt = /^(?:submit|button|image|reset|file)$/i,
			Nt = /^(?:input|select|textarea|keygen)/i;
			function Et(e, t, n, r) {
				var o;
				if (Array.isArray(t)) w.each(t, (function(t, o) {
					n || St.test(e) ? r(e, o) : Et(e + "[" + ("object" == typeof o && null != o ? t: "") + "]", o, n, r)
				}));
				else if (n || "object" !== x(t)) r(e, t);
				else for (o in t) Et(e + "[" + o + "]", t[o], n, r)
			}
			w.param = function(e, t) {
				var n, r = [],
				o = function(e, t) {
					var n = h(t) ? t() : t;
					r[r.length] = encodeURIComponent(e) + "=" + encodeURIComponent(null == n ? "": n)
				};
				if (null == e) return "";
				if (Array.isArray(e) || e.jquery && !w.isPlainObject(e)) w.each(e, (function() {
					o(this.name, this.value)
				}));
				else for (n in e) Et(n, e[n], t, o);
				return r.join("&")
			},
			w.fn.extend({
				serialize: function() {
					return w.param(this.serializeArray())
				},
				serializeArray: function() {
					return this.map((function() {
						var e = w.prop(this, "elements");
						return e ? w.makeArray(e) : this
					})).filter((function() {
						var e = this.type;
						return this.name && !w(this).is(":disabled") && Nt.test(this.nodeName) && !Dt.test(e) && (this.checked || !de.test(e))
					})).map((function(e, t) {
						var n = w(this).val();
						return null == n ? null: Array.isArray(n) ? w.map(n, (function(e) {
							return {
								name: t.name,
								value: e.replace(Ct, "\r\n")
							}
						})) : {
							name: t.name,
							value: n.replace(Ct, "\r\n")
						}
					})).get()
				}
			});
			var Ot = /%20/g,
			kt = /#.*$/,
			jt = /([?&])_=[^&]*/,
			At = /^(.*?):[ \t]*([^\r\n]*)$/gm,
			It = /^(?:GET|HEAD)$/,
			Lt = /^\/\//,
			qt = {},
			Rt = {},
			Ht = "*/".concat("*"),
			Ft = v.createElement("a");
			function Pt(e) {
				return function(t, n) {
					"string" != typeof t && (n = t, t = "*");
					var r, o = 0,
					i = t.toLowerCase().match(R) || [];
					if (h(n)) for (; r = i[o++];)"+" === r[0] ? (r = r.slice(1) || "*", (e[r] = e[r] || []).unshift(n)) : (e[r] = e[r] || []).push(n)
				}
			}
			function Mt(e, t, n, r) {
				var o = {},
				i = e === Rt;
				function a(s) {
					var u;
					return o[s] = !0,
					w.each(e[s] || [], (function(e, s) {
						var l = s(t, n, r);
						return "string" != typeof l || i || o[l] ? i ? !(u = l) : void 0 : (t.dataTypes.unshift(l), a(l), !1)
					})),
					u
				}
				return a(t.dataTypes[0]) || !o["*"] && a("*")
			}
			function Bt(e, t) {
				var n, r, o = w.ajaxSettings.flatOptions || {};
				for (n in t) void 0 !== t[n] && ((o[n] ? e: r || (r = {}))[n] = t[n]);
				return r && w.extend(!0, e, r),
				e
			}
			Ft.href = bt.href,
			w.extend({
				active: 0,
				lastModified: {},
				etag: {},
				ajaxSettings: {
					url: bt.href,
					type: "GET",
					isLocal: /^(?:about|app|app-storage|.+-extension|file|res|widget):$/.test(bt.protocol),
					global: !0,
					processData: !0,
					async: !0,
					contentType: "application/x-www-form-urlencoded; charset=UTF-8",
					accepts: {
						"*": Ht,
						text: "text/plain",
						html: "text/html",
						xml: "application/xml, text/xml",
						json: "application/json, text/javascript"
					},
					contents: {
						xml: /\bxml\b/,
						html: /\bhtml/,
						json: /\bjson\b/
					},
					responseFields: {
						xml: "responseXML",
						text: "responseText",
						json: "responseJSON"
					},
					converters: {
						"* text": String,
						"text html": !0,
						"text json": JSON.parse,
						"text xml": w.parseXML
					},
					flatOptions: {
						url: !0,
						context: !0
					}
				},
				ajaxSetup: function(e, t) {
					return t ? Bt(Bt(e, w.ajaxSettings), t) : Bt(w.ajaxSettings, e)
				},
				ajaxPrefilter: Pt(qt),
				ajaxTransport: Pt(Rt),
				ajax: function(t, n) {
					"object" == typeof t && (n = t, t = void 0),
					n = n || {};
					var r, o, i, a, s, u, l, c, f, p, d = w.ajaxSetup({},
					n),
					h = d.context || d,
					g = d.context && (h.nodeType || h.jquery) ? w(h) : w.event,
					y = w.Deferred(),
					m = w.Callbacks("once memory"),
					x = d.statusCode || {},
					b = {},
					T = {},
					S = "canceled",
					C = {
						readyState: 0,
						getResponseHeader: function(e) {
							var t;
							if (l) {
								if (!a) for (a = {}; t = At.exec(i);) a[t[1].toLowerCase() + " "] = (a[t[1].toLowerCase() + " "] || []).concat(t[2]);
								t = a[e.toLowerCase() + " "]
							}
							return null == t ? null: t.join(", ")
						},
						getAllResponseHeaders: function() {
							return l ? i: null
						},
						setRequestHeader: function(e, t) {
							return null == l && (e = T[e.toLowerCase()] = T[e.toLowerCase()] || e, b[e] = t),
							this
						},
						overrideMimeType: function(e) {
							return null == l && (d.mimeType = e),
							this
						},
						statusCode: function(e) {
							var t;
							if (e) if (l) C.always(e[C.status]);
							else for (t in e) x[t] = [x[t], e[t]];
							return this
						},
						abort: function(e) {
							var t = e || S;
							return r && r.abort(t),
							D(0, t),
							this
						}
					};
					if (y.promise(C), d.url = ((t || d.url || bt.href) + "").replace(Lt, bt.protocol + "//"), d.type = n.method || n.type || d.method || d.type, d.dataTypes = (d.dataType || "*").toLowerCase().match(R) || [""], null == d.crossDomain) {
						u = v.createElement("a");
						try {
							u.href = d.url,
							u.href = u.href,
							d.crossDomain = Ft.protocol + "//" + Ft.host != u.protocol + "//" + u.host
						} catch(e) {
							d.crossDomain = !0
						}
					}
					if (d.data && d.processData && "string" != typeof d.data && (d.data = w.param(d.data, d.traditional)), Mt(qt, d, n, C), l) return C;
					for (f in (c = w.event && d.global) && 0 == w.active++&&w.event.trigger("ajaxStart"), d.type = d.type.toUpperCase(), d.hasContent = !It.test(d.type), o = d.url.replace(kt, ""), d.hasContent ? d.data && d.processData && 0 === (d.contentType || "").indexOf("application/x-www-form-urlencoded") && (d.data = d.data.replace(Ot, "+")) : (p = d.url.slice(o.length), d.data && (d.processData || "string" == typeof d.data) && (o += (Tt.test(o) ? "&": "?") + d.data, delete d.data), !1 === d.cache && (o = o.replace(jt, "$1"), p = (Tt.test(o) ? "&": "?") + "_=" + wt.guid+++p), d.url = o + p), d.ifModified && (w.lastModified[o] && C.setRequestHeader("If-Modified-Since", w.lastModified[o]), w.etag[o] && C.setRequestHeader("If-None-Match", w.etag[o])), (d.data && d.hasContent && !1 !== d.contentType || n.contentType) && C.setRequestHeader("Content-Type", d.contentType), C.setRequestHeader("Accept", d.dataTypes[0] && d.accepts[d.dataTypes[0]] ? d.accepts[d.dataTypes[0]] + ("*" !== d.dataTypes[0] ? ", " + Ht + "; q=0.01": "") : d.accepts["*"]), d.headers) C.setRequestHeader(f, d.headers[f]);
					if (d.beforeSend && (!1 === d.beforeSend.call(h, C, d) || l)) return C.abort();
					if (S = "abort", m.add(d.complete), C.done(d.success), C.fail(d.error), r = Mt(Rt, d, n, C)) {
						if (C.readyState = 1, c && g.trigger("ajaxSend", [C, d]), l) return C;
						d.async && d.timeout > 0 && (s = e.setTimeout((function() {
							C.abort("timeout")
						}), d.timeout));
						try {
							l = !1,
							r.send(b, D)
						} catch(e) {
							if (l) throw e;
							D(-1, e)
						}
					} else D(-1, "No Transport");
					function D(t, n, a, u) {
						var f, p, v, b, T, S = n;
						l || (l = !0, s && e.clearTimeout(s), r = void 0, i = u || "", C.readyState = t > 0 ? 4 : 0, f = t >= 200 && t < 300 || 304 === t, a && (b = function(e, t, n) {
							for (var r, o, i, a, s = e.contents,
							u = e.dataTypes;
							"*" === u[0];) u.shift(),
							void 0 === r && (r = e.mimeType || t.getResponseHeader("Content-Type"));
							if (r) for (o in s) if (s[o] && s[o].test(r)) {
								u.unshift(o);
								break
							}
							if (u[0] in n) i = u[0];
							else {
								for (o in n) {
									if (!u[0] || e.converters[o + " " + u[0]]) {
										i = o;
										break
									}
									a || (a = o)
								}
								i = i || a
							}
							if (i) return i !== u[0] && u.unshift(i),
							n[i]
						} (d, C, a)), !f && w.inArray("script", d.dataTypes) > -1 && w.inArray("json", d.dataTypes) < 0 && (d.converters["text script"] = function() {}), b = function(e, t, n, r) {
							var o, i, a, s, u, l = {},
							c = e.dataTypes.slice();
							if (c[1]) for (a in e.converters) l[a.toLowerCase()] = e.converters[a];
							for (i = c.shift(); i;) if (e.responseFields[i] && (n[e.responseFields[i]] = t), !u && r && e.dataFilter && (t = e.dataFilter(t, e.dataType)), u = i, i = c.shift()) if ("*" === i) i = u;
							else if ("*" !== u && u !== i) {
								if (! (a = l[u + " " + i] || l["* " + i])) for (o in l) if ((s = o.split(" "))[1] === i && (a = l[u + " " + s[0]] || l["* " + s[0]])) { ! 0 === a ? a = l[o] : !0 !== l[o] && (i = s[0], c.unshift(s[1]));
									break
								}
								if (!0 !== a) if (a && e.throws) t = a(t);
								else try {
									t = a(t)
								} catch(e) {
									return {
										state: "parsererror",
										error: a ? e: "No conversion from " + u + " to " + i
									}
								}
							}
							return {
								state: "success",
								data: t
							}
						} (d, b, C, f), f ? (d.ifModified && ((T = C.getResponseHeader("Last-Modified")) && (w.lastModified[o] = T), (T = C.getResponseHeader("etag")) && (w.etag[o] = T)), 204 === t || "HEAD" === d.type ? S = "nocontent": 304 === t ? S = "notmodified": (S = b.state, p = b.data, f = !(v = b.error))) : (v = S, !t && S || (S = "error", t < 0 && (t = 0))), C.status = t, C.statusText = (n || S) + "", f ? y.resolveWith(h, [p, S, C]) : y.rejectWith(h, [C, S, v]), C.statusCode(x), x = void 0, c && g.trigger(f ? "ajaxSuccess": "ajaxError", [C, d, f ? p: v]), m.fireWith(h, [C, S]), c && (g.trigger("ajaxComplete", [C, d]), --w.active || w.event.trigger("ajaxStop")))
					}
					return C
				},
				getJSON: function(e, t, n) {
					return w.get(e, t, n, "json")
				},
				getScript: function(e, t) {
					return w.get(e, void 0, t, "script")
				}
			}),
			w.each(["get", "post"], (function(e, t) {
				w[t] = function(e, n, r, o) {
					return h(n) && (o = o || r, r = n, n = void 0),
					w.ajax(w.extend({
						url: e,
						type: t,
						dataType: o,
						data: n,
						success: r
					},
					w.isPlainObject(e) && e))
				}
			})),
			w.ajaxPrefilter((function(e) {
				var t;
				for (t in e.headers)"content-type" === t.toLowerCase() && (e.contentType = e.headers[t] || "")
			})),
			w._evalUrl = function(e, t, n) {
				return w.ajax({
					url: e,
					type: "GET",
					dataType: "script",
					cache: !0,
					async: !1,
					global: !1,
					converters: {
						"text script": function() {}
					},
					dataFilter: function(e) {
						w.globalEval(e, t, n)
					}
				})
			},
			w.fn.extend({
				wrapAll: function(e) {
					var t;
					return this[0] && (h(e) && (e = e.call(this[0])), t = w(e, this[0].ownerDocument).eq(0).clone(!0), this[0].parentNode && t.insertBefore(this[0]), t.map((function() {
						for (var e = this; e.firstElementChild;) e = e.firstElementChild;
						return e
					})).append(this)),
					this
				},
				wrapInner: function(e) {
					return h(e) ? this.each((function(t) {
						w(this).wrapInner(e.call(this, t))
					})) : this.each((function() {
						var t = w(this),
						n = t.contents();
						n.length ? n.wrapAll(e) : t.append(e)
					}))
				},
				wrap: function(e) {
					var t = h(e);
					return this.each((function(n) {
						w(this).wrapAll(t ? e.call(this, n) : e)
					}))
				},
				unwrap: function(e) {
					return this.parent(e).not("body").each((function() {
						w(this).replaceWith(this.childNodes)
					})),
					this
				}
			}),
			w.expr.pseudos.hidden = function(e) {
				return ! w.expr.pseudos.visible(e)
			},
			w.expr.pseudos.visible = function(e) {
				return !! (e.offsetWidth || e.offsetHeight || e.getClientRects().length)
			},
			w.ajaxSettings.xhr = function() {
				try {
					return new e.XMLHttpRequest
				} catch(e) {}
			};
			var $t = {
				0 : 200,
				1223 : 204
			},
			Wt = w.ajaxSettings.xhr();
			d.cors = !!Wt && "withCredentials" in Wt,
			d.ajax = Wt = !!Wt,
			w.ajaxTransport((function(t) {
				var n, r;
				if (d.cors || Wt && !t.crossDomain) return {
					send: function(o, i) {
						var a, s = t.xhr();
						if (s.open(t.type, t.url, t.async, t.username, t.password), t.xhrFields) for (a in t.xhrFields) s[a] = t.xhrFields[a];
						for (a in t.mimeType && s.overrideMimeType && s.overrideMimeType(t.mimeType), t.crossDomain || o["X-Requested-With"] || (o["X-Requested-With"] = "XMLHttpRequest"), o) s.setRequestHeader(a, o[a]);
						n = function(e) {
							return function() {
								n && (n = r = s.onload = s.onerror = s.onabort = s.ontimeout = s.onreadystatechange = null, "abort" === e ? s.abort() : "error" === e ? "number" != typeof s.status ? i(0, "error") : i(s.status, s.statusText) : i($t[s.status] || s.status, s.statusText, "text" !== (s.responseType || "text") || "string" != typeof s.responseText ? {
									binary: s.response
								}: {
									text: s.responseText
								},
								s.getAllResponseHeaders()))
							}
						},
						s.onload = n(),
						r = s.onerror = s.ontimeout = n("error"),
						void 0 !== s.onabort ? s.onabort = r: s.onreadystatechange = function() {
							4 === s.readyState && e.setTimeout((function() {
								n && r()
							}))
						},
						n = n("abort");
						try {
							s.send(t.hasContent && t.data || null)
						} catch(e) {
							if (n) throw e
						}
					},
					abort: function() {
						n && n()
					}
				}
			})),
			w.ajaxPrefilter((function(e) {
				e.crossDomain && (e.contents.script = !1)
			})),
			w.ajaxSetup({
				accepts: {
					script: "text/javascript, application/javascript, application/ecmascript, application/x-ecmascript"
				},
				contents: {
					script: /\b(?:java|ecma)script\b/
				},
				converters: {
					"text script": function(e) {
						return w.globalEval(e),
						e
					}
				}
			}),
			w.ajaxPrefilter("script", (function(e) {
				void 0 === e.cache && (e.cache = !1),
				e.crossDomain && (e.type = "GET")
			})),
			w.ajaxTransport("script", (function(e) {
				var t, n;
				if (e.crossDomain || e.scriptAttrs) return {
					send: function(r, o) {
						t = w("<script>").attr(e.scriptAttrs || {}).prop({
							charset: e.scriptCharset,
							src: e.url
						}).on("load error", n = function(e) {
							t.remove(),
							n = null,
							e && o("error" === e.type ? 404 : 200, e.type)
						}),
						v.head.appendChild(t[0])
					},
					abort: function() {
						n && n()
					}
				}
			}));
			var _t, Ut = [],
			Jt = /(=)\?(?=&|$)|\?\?/;
			w.ajaxSetup({
				jsonp: "callback",
				jsonpCallback: function() {
					var e = Ut.pop() || w.expando + "_" + wt.guid++;
					return this[e] = !0,
					e
				}
			}),
			w.ajaxPrefilter("json jsonp", (function(t, n, r) {
				var o, i, a, s = !1 !== t.jsonp && (Jt.test(t.url) ? "url": "string" == typeof t.data && 0 === (t.contentType || "").indexOf("application/x-www-form-urlencoded") && Jt.test(t.data) && "data");
				if (s || "jsonp" === t.dataTypes[0]) return o = t.jsonpCallback = h(t.jsonpCallback) ? t.jsonpCallback() : t.jsonpCallback,
				s ? t[s] = t[s].replace(Jt, "$1" + o) : !1 !== t.jsonp && (t.url += (Tt.test(t.url) ? "&": "?") + t.jsonp + "=" + o),
				t.converters["script json"] = function() {
					return a || w.error(o + " was not called"),
					a[0]
				},
				t.dataTypes[0] = "json",
				i = e[o],
				e[o] = function() {
					a = arguments
				},
				r.always((function() {
					void 0 === i ? w(e).removeProp(o) : e[o] = i,
					t[o] && (t.jsonpCallback = n.jsonpCallback, Ut.push(o)),
					a && h(i) && i(a[0]),
					a = i = void 0
				})),
				"script"
			})),
			d.createHTMLDocument = ((_t = v.implementation.createHTMLDocument("").body).innerHTML = "<form></form><form></form>", 2 === _t.childNodes.length),
			w.parseHTML = function(e, t, n) {
				return "string" != typeof e ? [] : ("boolean" == typeof t && (n = t, t = !1), t || (d.createHTMLDocument ? ((r = (t = v.implementation.createHTMLDocument("")).createElement("base")).href = v.location.href, t.head.appendChild(r)) : t = v), i = !n && [], (o = O.exec(e)) ? [t.createElement(o[1])] : (o = be([e], t, i), i && i.length && w(i).remove(), w.merge([], o.childNodes)));
				var r, o, i
			}, w.fn.load = function(e, t, n) {
				var r, o, i, a = this,
				s = e.indexOf(" ");
				return s > -1 && (r = ht(e.slice(s)), e = e.slice(0, s)),
				h(t) ? (n = t, t = void 0) : t && "object" == typeof t && (o = "POST"),
				a.length > 0 && w.ajax({
					url: e,
					type: o || "GET",
					dataType: "html",
					data: t
				}).done((function(e) {
					i = arguments,
					a.html(r ? w("<div>").append(w.parseHTML(e)).find(r) : e)
				})).always(n &&
				function(e, t) {
					a.each((function() {
						n.apply(this, i || [e.responseText, t, e])
					}))
				}),
				this
			},
			w.expr.pseudos.animated = function(e) {
				return w.grep(w.timers, (function(t) {
					return e === t.elem
				})).length
			},
			w.offset = {
				setOffset: function(e, t, n) {
					var r, o, i, a, s, u, l = w.css(e, "position"),
					c = w(e),
					f = {};
					"static" === l && (e.style.position = "relative"),
					s = c.offset(),
					i = w.css(e, "top"),
					u = w.css(e, "left"),
					("absolute" === l || "fixed" === l) && (i + u).indexOf("auto") > -1 ? (a = (r = c.position()).top, o = r.left) : (a = parseFloat(i) || 0, o = parseFloat(u) || 0),
					h(t) && (t = t.call(e, n, w.extend({},
					s))),
					null != t.top && (f.top = t.top - s.top + a),
					null != t.left && (f.left = t.left - s.left + o),
					"using" in t ? t.using.call(e, f) : c.css(f)
				}
			},
			w.fn.extend({
				offset: function(e) {
					if (arguments.length) return void 0 === e ? this: this.each((function(t) {
						w.offset.setOffset(this, e, t)
					}));
					var t, n, r = this[0];
					return r ? r.getClientRects().length ? (t = r.getBoundingClientRect(), n = r.ownerDocument.defaultView, {
						top: t.top + n.pageYOffset,
						left: t.left + n.pageXOffset
					}) : {
						top: 0,
						left: 0
					}: void 0
				},
				position: function() {
					if (this[0]) {
						var e, t, n, r = this[0],
						o = {
							top: 0,
							left: 0
						};
						if ("fixed" === w.css(r, "position")) t = r.getBoundingClientRect();
						else {
							for (t = this.offset(), n = r.ownerDocument, e = r.offsetParent || n.documentElement; e && (e === n.body || e === n.documentElement) && "static" === w.css(e, "position");) e = e.parentNode;
							e && e !== r && 1 === e.nodeType && ((o = w(e).offset()).top += w.css(e, "borderTopWidth", !0), o.left += w.css(e, "borderLeftWidth", !0))
						}
						return {
							top: t.top - o.top - w.css(r, "marginTop", !0),
							left: t.left - o.left - w.css(r, "marginLeft", !0)
						}
					}
				},
				offsetParent: function() {
					return this.map((function() {
						for (var e = this.offsetParent; e && "static" === w.css(e, "position");) e = e.offsetParent;
						return e || re
					}))
				}
			}),
			w.each({
				scrollLeft: "pageXOffset",
				scrollTop: "pageYOffset"
			},
			(function(e, t) {
				var n = "pageYOffset" === t;
				w.fn[e] = function(r) {
					return W(this, (function(e, r, o) {
						var i;
						if (g(e) ? i = e: 9 === e.nodeType && (i = e.defaultView), void 0 === o) return i ? i[t] : e[r];
						i ? i.scrollTo(n ? i.pageXOffset: o, n ? o: i.pageYOffset) : e[r] = o
					}), e, r, arguments.length)
				}
			})),
			w.each(["top", "left"], (function(e, t) {
				w.cssHooks[t] = We(d.pixelPosition, (function(e, n) {
					if (n) return n = $e(e, t),
					Fe.test(n) ? w(e).position()[t] + "px": n
				}))
			})),
			w.each({
				Height: "height",
				Width: "width"
			},
			(function(e, t) {
				w.each({
					padding: "inner" + e,
					content: t,
					"": "outer" + e
				},
				(function(n, r) {
					w.fn[r] = function(o, i) {
						var a = arguments.length && (n || "boolean" != typeof o),
						s = n || (!0 === o || !0 === i ? "margin": "border");
						return W(this, (function(t, n, o) {
							var i;
							return g(t) ? 0 === r.indexOf("outer") ? t["inner" + e] : t.document.documentElement["client" + e] : 9 === t.nodeType ? (i = t.documentElement, Math.max(t.body["scroll" + e], i["scroll" + e], t.body["offset" + e], i["offset" + e], i["client" + e])) : void 0 === o ? w.css(t, n, s) : w.style(t, n, o, s)
						}), t, a ? o: void 0, a)
					}
				}))
			})),
			w.each(["ajaxStart", "ajaxStop", "ajaxComplete", "ajaxError", "ajaxSuccess", "ajaxSend"], (function(e, t) {
				w.fn[t] = function(e) {
					return this.on(t, e)
				}
			})),
			w.fn.extend({
				bind: function(e, t, n) {
					return this.on(e, null, t, n)
				},
				unbind: function(e, t) {
					return this.off(e, null, t)
				},
				delegate: function(e, t, n, r) {
					return this.on(t, e, n, r)
				},
				undelegate: function(e, t, n) {
					return 1 === arguments.length ? this.off(e, "**") : this.off(t, e || "**", n)
				},
				hover: function(e, t) {
					return this.mouseenter(e).mouseleave(t || e)
				}
			}),
			w.each("blur focus focusin focusout resize scroll click dblclick mousedown mouseup mousemove mouseover mouseout mouseenter mouseleave change select submit keydown keypress keyup contextmenu".split(" "), (function(e, t) {
				w.fn[t] = function(e, n) {
					return arguments.length > 0 ? this.on(t, null, e, n) : this.trigger(t)
				}
			}));
			var zt = /^[\s\uFEFF\xA0]+|[\s\uFEFF\xA0]+$/g;
			w.proxy = function(e, t) {
				var n, r, i;
				if ("string" == typeof t && (n = e[t], t = e, e = n), h(e)) return r = o.call(arguments, 2),
				i = function() {
					return e.apply(t || this, r.concat(o.call(arguments)))
				},
				i.guid = e.guid = e.guid || w.guid++,
				i
			},
			w.holdReady = function(e) {
				e ? w.readyWait++:w.ready(!0)
			},
			w.isArray = Array.isArray,
			w.parseJSON = JSON.parse,
			w.nodeName = E,
			w.isFunction = h,
			w.isWindow = g,
			w.camelCase = z,
			w.type = x,
			w.now = Date.now,
			w.isNumeric = function(e) {
				var t = w.type(e);
				return ("number" === t || "string" === t) && !isNaN(e - parseFloat(e))
			},
			w.trim = function(e) {
				return null == e ? "": (e + "").replace(zt, "")
			};
			var Xt = e.jQuery,
			Gt = e.$;
			return w.noConflict = function(t) {
				return e.$ === w && (e.$ = Gt),
				t && e.jQuery === w && (e.jQuery = Xt),
				w
			},
			void 0 === t && (e.jQuery = e.$ = w),
			w
		}))
	} (n);
	var r, o, i, a, s, l, c, f = n.exports,
	p = function(e, t) {
		console.log(arguments[0], t)
	},
	d = function(e) {
		p(e = "%c" + e, "margin:10px 0;font-size: 14px;font-weight: 400;background: linear-gradient(-225deg, #26a7ff 0%, #0066ff 40%);color:#fff;padding:6px 12px;border-radius:0px;")
	},
	h = function(e) {
		p(e = "%c错误信息：" + e, "margin:10px 0;font-size: 14px;font-weight: 400;background: linear-gradient(-225deg, #fb9891 0%, #d54439 40%);color:#fff;padding:6px 12px;border-radius:0px;")
	},
	g = window.location.protocol + "//" + window.location.host,
	v = j("token"),
	y = j("sequenceCode");
	function m(e, t, n, r, o) {
		d("【ReciveData】传入数据："),
		console.table({
			moduleFlag: e,
			questionNumber: t,
			questionStem: n,
			scores: r,
			isTrue: o
		}),
		y ? (d("新平台对接，进入新平台对接接口调用"), C(e, t, n, r, o)) : (d("旧平台对接，进入分数对接接口调用"), w(r[0]))
	}
	function x() {
		var e = {
			eid: i,
			role: a,
			numberId: s,
			name: l,
			className: c
		};
		return d("【getUserInfo】获取用户信息："),
		console.table(e),
		e
	}
	function b(e, t) {
		var n = {
			eid: i,
			role: a,
			numberId: s,
			name: l,
			className: c
		};
		d("【getUserInfoFn】unity获取用户信息,传入参数 GameObjectName:" + e + "MethodName:" + t),
		window.gameInstance.SendMessage(e, t, JSON.stringify(n))
	}
	function w(e) {
		d("【expScoreSave】上传实验成绩接口接收数据为:" + e);
		var t = {
			eid: i,
			expScore: e
		},
		n = new FormData;
		n.append("param", JSON.stringify(t)),
		f.ajax({
			timeout: 2e4,
			type: "POST",
			url: g + "/virexp/outer/intelligent/!expScoreSave",
			data: n,
			contentType: !1,
			processData: !1,
			success: function(e) {
				d("【expScoreSave】分数上传成功"),
				console.log(e)
			},
			error: function(e, t, n) {
				h("【expScoreSave】分数接口调用失败，请联系项目负责人"),
				console.log(e)
			}
		})
	}
	function T(e) {
		d("【ReportEdit】开始上传实验报告"),
		console.table(Object.assign({
			"【ReportEdit】": "传入参数",
			report: e
		}));
		var t = JSON.parse(e);
		t.eid = i;
		var n = new FormData;
		n.append("param", JSON.stringify(t)),
		f.ajax({
			timeout: 2e4,
			type: "POST",
			url: g + "/virexp/outer/report/!reportEdit",
			data: n,
			contentType: !1,
			processData: !1,
			success: function(e) {
				d("实验报告接口调用完成，返回参数为："),
				console.table(e)
			},
			error: function(e, t, n) {
				h("接口调用失败，请联系项目负责人"),
				console.log(e)
			}
		})
	}
	function S(e) {
		d("【UploadSteps】开始上传实验步骤"),
		console.table(Object.assign({
			"【UploadSteps】": "传入参数",
			report: e
		}));
		var t = JSON.parse(e);
		t.eid = i;
		var n = new FormData;
		n.append("param", JSON.stringify(t)),
		f.ajax({
			timeout: 2e4,
			type: "POST",
			url: g + "/virexp/outer/report/!uploadSteps",
			data: n,
			contentType: !1,
			processData: !1,
			success: function(e) {
				d("实验步骤接口调用完成，返回参数为："),
				console.table(e)
			},
			error: function(e, t, n) {
				h("实验步骤接口调用失败，请联系项目负责人"),
				console.log(e)
			}
		})
	}
	function C(e, t, n, r, o) {
		var i = window.location.protocol + "//" + window.location.host,
		a = document.referrer;
		a && (-1 != a.indexOf("http://") ? i = a.substring(0, a.indexOf("/", a.indexOf("http://") + 7)) : -1 != a.indexOf("https://") && (i = a.substring(0, a.indexOf("/", a.indexOf("https://") + 8))));
		var s = null,
		u = new RegExp("(^|&)token=([^&]*)(&|$)"),
		l = window.location.search.substr(1).match(u);
		null != l && (s = unescape(l[2]).split("_"));
		var c = null,
		p = new RegExp("(^|&)key=([^&]*)(&|$)"),
		d = window.location.search.substr(1).match(p);
		null != d && (c = unescape(d[2]));
		var h = null,
		g = new RegExp("(^|&)host=([^&]*)(&|$)"),
		v = window.location.search.substr(1).match(g);
		null != v && (h = unescape(v[2])),
		h && (i = h),
		f.getJSON(i + "/openapi/" + s[0] + "/" + s[1], (function(a) {
			a.name,
			a.token;
			var u = i + a.urlDataPost; (new JSEncrypt).setPublicKey(c);
			for (var l = new Array,
			p = 0; p < e.length; p++) l.push({
				moduleFlag: e[p],
				questionNumber: parseInt(t[p]),
				questionStem: n[p],
				score: r[p],
				trueOrFalse: o[p]
			});
			f.ajax({
				timeout: 2e4,
				type: "POST",
				dataType: "JSON",
				contentType: "application/json;charset=UTF-8",
				url: u + "/" + s[0] + "/" + s[1],
				data: JSON.stringify(l),
				success: function(e) {
					console.log("调用接口成功"),
					console.log(e)
				},
				error: function(e, t, n) {
					console.log(e.responseText)
				}
			})
		}))
	}
	function D(e) {
		if (d("【getBaseInfo】资源对接开始,进行接口调用所需参数回传"), 2 == arguments.length) var t = arguments[0],
		n = arguments[1];
		else {
			var r = eval;
			"string" == typeof e && (e = r("(" + e + ")"));
			t = e.objname,
			n = e.methodname
		}
		var o = document.referrer;
		o && (-1 != o.indexOf("http://") ? g = o.substring(0, o.indexOf("/", o.indexOf("http://") + 7)) : -1 != o.indexOf("https://") && (g = o.substring(0, o.indexOf("/", o.indexOf("https://") + 8))));
		var i = null,
		a = new RegExp("(^|&)host=([^&]*)(&|$)"),
		s = window.location.search.substr(1).match(a);
		if (null != s && (i = unescape(s[2])), i && (g = i), y) {
			d("【getBaseInfo】新平台对接，进行接口调用所需参数回传");
			try {
				var l = v.split("_"),
				c = l[0],
				f = l[1],
				p = j("un"),
				m = j("code"),
				x = {
					appId: c,
					expId: f,
					sequenceCode: y,
					host: g,
					userName: p,
					userCode: m
				}
			} catch(e) {
				x = {},
				h("【getBaseInfo】token错误，请检查")
			}
			if (window.u) d("【getBaseInfo】webplayer返回:" + JSON.stringify(x)),
			u.getUnity().SendMessage(t, n, JSON.stringify(x));
			else if (window.gameInstance) d("【getBaseInfo】webGL返回:" + JSON.stringify(x)),
			window.gameInstance.SendMessage(t, n, JSON.stringify(x));
			else {
				if (!window.unityInstance) return d("【getBaseInfo】纯客户端返回:" + JSON.stringify(x)),
				e.success && e.success(x),
				x;
				if (d("【getBaseInfo】webGL返回:" + JSON.stringify(x)), e.sendUnity) return e.success && e.success(x),
				x;
				window.unityInstance.SendMessage(t, n, JSON.stringify(x))
			}
		} else h("【getBaseInfo】旧平台对接，进入新平台对接接口调用"),
		window.parent.postMessage("getUserInfo", "*")
	}
	function N(e) {
		var t = eval;
		"string" == typeof e && (e = t("(" + e + ")")),
		d("【uploadResultData】实验结果上传接口调用"),
		console.table(Object.assign({
			"【uploadResultData】": "传入参数数据"
		},
		e));
		var n = e.objname,
		r = e.methodname,
		o = document.referrer;
		o && (-1 != o.indexOf("http://") ? g = o.substring(0, o.indexOf("/", o.indexOf("http://") + 7)) : -1 != o.indexOf("https://") && (g = o.substring(0, o.indexOf("/", o.indexOf("https://") + 8))));
		var i = null,
		a = new RegExp("(^|&)host=([^&]*)(&|$)"),
		s = window.location.search.substr(1).match(a);
		if (null != s && (i = unescape(s[2])), i && (g = i), y) {
			d("【uploadResultData】新平台对接，进行接口调用所需参数回传");
			try {
				if (!e.appId || !e.expId) {
					var l = v.split("_");
					e.appId = e.appId || l[0],
					e.expId = e.expId || l[1]
				}
			} catch(e) {
				h("【uploadResultData】token错误，请检查")
			}
			var c = {
				appId: e.appId || "",
				expId: e.expId || "",
				version: e.version || "1.0",
				reportData: e.reportData || "",
				expScoreDetails: e.expScoreDetails || "",
				expScriptContent: e.expScriptContent || ""
			};
			f.ajax({
				timeout: 2e4,
				type: "POST",
				dataType: "JSON",
				contentType: "application/json;charset=UTF-8",
				url: g + "/openapi/data_upload",
				data: JSON.stringify(c),
				success: function(t) {
					d("【uploadResultData】调用接口成功"),
					console.table(t),
					window.u ? (d("【uploadResultData】webplayer返回:"), u.getUnity().SendMessage(n, r, JSON.stringify(t))) : window.gameInstance ? (d("【uploadResultData】webGL返回:"), window.gameInstance.SendMessage(n, r, JSON.stringify(t))) : window.unityInstance ? (d("【uploadResultData】webGL返回:"), window.unityInstance.SendMessage(n, r, JSON.stringify(t))) : (d("【uploadResultData】纯网页客户端返回:"), e.success && e.success(t))
				},
				error: function(t, n, r) {
					h("【uploadResultData】接口调用失败请检查接口是否存在"),
					e.error && e.error(t)
				}
			})
		} else h("【uploadResultData】旧平台对接，进入新平台对接接口调用")
	}
	function E(e) {
		var t = eval;
		"string" == typeof e && (e = t("(" + e + ")")),
		d("【uploadFile】附件上传接口接口调用"),
		console.table(Object.assign({
			"【uploadFile】": "传入参数数据"
		},
		e));
		var n = e.objname,
		r = e.methodname,
		o = document.referrer;
		o && (-1 != o.indexOf("http://") ? g = o.substring(0, o.indexOf("/", o.indexOf("http://") + 7)) : -1 != o.indexOf("https://") && (g = o.substring(0, o.indexOf("/", o.indexOf("https://") + 8))));
		var i = null,
		a = new RegExp("(^|&)host=([^&]*)(&|$)"),
		s = window.location.search.substr(1).match(a);
		if (null != s && (i = unescape(s[2])), i && (g = i), y) {
			d("【uploadFile】新平台对接，进行接口调用所需参数回传");
			try {
				if (!e.appId || !e.expId) {
					var l = v.split("_");
					e.appId = e.appId || l[0],
					e.expId = e.expId || l[1]
				}
			} catch(e) {
				h("【uploadFile】token错误，请检查")
			}
			var c = new FormData;
			c.append("appId", e.appId),
			c.append("expId", e.expId);
			for (var p = 0; p < e.fileList.length; p++) c.append("fileList", e.fileList[p]);
			f.ajax({
				type: "POST",
				url: g + "/openapi/upload_file",
				data: c,
				contentType: !1,
				processData: !1,
				success: function(t) {
					d("【uploadFile】调用接口成功"),
					console.table(t),
					window.u ? (d("【uploadFile】webplayer返回:"), u.getUnity().SendMessage(n, r, JSON.stringify(t))) : window.gameInstance ? (d("【uploadFile】webGL返回:"), window.gameInstance.SendMessage(n, r, JSON.stringify(t))) : window.unityInstance ? (d("【uploadFile】webGL返回:"), window.unityInstance.SendMessage(n, r, JSON.stringify(t))) : (d("【uploadFile】纯网页客户端返回:"), e.success && e.success(t))
				},
				error: function(t, n, r) {
					h("【uploadFile】接口调用失败请检查接口是否存在"),
					e.error && e.error(t)
				}
			})
		} else h("【uploadFile】旧平台对接，进入新平台对接接口调用")
	}
	function O(e) {
		var t = eval;
		"string" == typeof e && (e = t("(" + e + ")")),
		d("【uploadFileBase64】附件上传接口接口调用"),
		console.table(Object.assign({
			"【uploadFileBase64】": "传入参数数据"
		},
		e));
		var n = e.objname,
		r = e.methodname,
		o = document.referrer;
		o && (-1 != o.indexOf("http://") ? g = o.substring(0, o.indexOf("/", o.indexOf("http://") + 7)) : -1 != o.indexOf("https://") && (g = o.substring(0, o.indexOf("/", o.indexOf("https://") + 8))));
		var i = null,
		a = new RegExp("(^|&)host=([^&]*)(&|$)"),
		s = window.location.search.substr(1).match(a);
		if (null != s && (i = unescape(s[2])), i && (g = i), y) {
			d("【uploadFileBase64】新平台对接，进行接口调用所需参数回传");
			try {
				if (!e.appId || !e.expId) {
					var l = v.split("_");
					e.appId = e.appId || l[0],
					e.expId = e.expId || l[1]
				}
			} catch(e) {
				h("【uploadFileBase64】token错误，请检查")
			}
			var c = {
				appId: e.appId || "",
				expId: e.expId || "",
				version: e.version || "1.0"
			};
			e.imageDetails && (c.imageDetails = e.imageDetails),
			e.videoDetails && (c.videoDetails = e.videoDetails),
			e.audioDetails && (c.audioDetails = e.audioDetails),
			f.ajax({
				timeout: 2e4,
				type: "POST",
				dataType: "JSON",
				contentType: "application/json;charset=UTF-8",
				url: g + "/openapi/file_upload",
				data: JSON.stringify(c),
				success: function(t) {
					d("【uploadFileBase64】调用接口成功"),
					console.table(t),
					window.u ? (d("【uploadFileBase64】webplayer返回:"), u.getUnity().SendMessage(n, r, JSON.stringify(t))) : window.gameInstance ? (d("【uploadFileBase64】webGL返回:"), window.gameInstance.SendMessage(n, r, JSON.stringify(t))) : window.unityInstance ? (d("【uploadFileBase64】webGL返回:"), window.unityInstance.SendMessage(n, r, JSON.stringify(t))) : (d("【uploadFileBase64】纯网页客户端返回:"), e.success && e.success(t))
				},
				error: function(t, n, r) {
					h("【uploadFileBase64】接口调用失败请检查接口是否存在"),
					e.error && e.error(t)
				}
			})
		} else h("【uploadFileBase64】旧平台对接，进入新平台对接接口调用")
	}
	function k(e) {
		var t = eval;
		"string" == typeof e && (e = t("(" + e + ")")),
		"string" == typeof e && (e = JSON.parse(e)),
		d("【downloadData】实验数据获取接口调用"),
		console.table(Object.assign({
			"【downloadData】": "传入参数数据"
		},
		e));
		var n = e.objname,
		r = e.methodname,
		o = document.referrer;
		o && (-1 != o.indexOf("http://") ? g = o.substring(0, o.indexOf("/", o.indexOf("http://") + 7)) : -1 != o.indexOf("https://") && (g = o.substring(0, o.indexOf("/", o.indexOf("https://") + 8))));
		var i = null,
		a = new RegExp("(^|&)host=([^&]*)(&|$)"),
		s = window.location.search.substr(1).match(a);
		if (null != s && (i = unescape(s[2])), i && (g = i), y) {
			d("【downloadData】新平台对接，进行接口调用所需参数回传");
			try {
				if (!e.appId || !e.expId) {
					var l = v.split("_");
					e.appId = e.appId || l[0],
					e.expId = e.expId || l[1]
				}
			} catch(e) {
				h("【downloadData】token错误，请检查")
			}
			var c = {
				appId: e.appId || "",
				expId: e.expId || "",
				dataType: e.dataType || ""
			};
			f.ajax({
				type: "get",
				url: g + "/openapi/data_get",
				data: c,
				success: function(t) {
					d("【downloadData】调用接口成功"),
					console.table(t),
					window.u ? (d("【downloadData】webplayer返回:"), u.getUnity().SendMessage(n, r, JSON.stringify(t))) : window.gameInstance ? (d("【downloadData】webGL返回:"), window.gameInstance.SendMessage(n, r, JSON.stringify(t))) : window.unityInstance ? (d("【downloadData】webGL返回:"), window.unityInstance.SendMessage(n, r, JSON.stringify(t))) : (d("【downloadData】纯网页客户端返回:"), e.success && e.success(t))
				},
				error: function(t, n, r) {
					h("【downloadData】接口调用失败请检查接口是否存在"),
					e.error && e.error(t)
				}
			})
		} else h("【downloadData】旧平台对接，进入新平台对接接口调用")
	}
	function j(e) {
		var t = window.location.href;
		if (!t.split("?")[1]) return null;
		for (var n = t.split("?")[1].split("&"), r = n.length, o = {},
		i = [], a = 0; a < r; a++) o[(i = n[a].split("="))[0]] = i[1];
		for (var s in o) if (s == e) return o[e]
	}
	j("host"),
	f((function() {
		if ((o = document.referrer) && (-1 != o.indexOf("http://") ? g = o.substring(0, o.indexOf("/", o.indexOf("http://") + 7)) : -1 != o.indexOf("https://") && (g = o.substring(0, o.indexOf("/", o.indexOf("https://") + 8)))), !y) {
			t();
			var e = setInterval((function() {
				t()
			}), 3e3)
		}
		function t() {
			i && "undefined" != i ? (console.log("已获取到eid" + i), clearInterval(e)) : (console.log("获取eid调用监听,暂未获取eid"), r = !0, window.parent.postMessage("getUserInfo", "*"))
		}
	})),
	window.addEventListener("message", (function(e) {
		var t = e.data;
		t.eid && r && (r = !1, i = t.eid, a = t.role, s = t.numberId, l = t.name, c = t.className, console.log("outer.js老平台获取eid成功：" + i))
	}), !1),
	function(e) {
		e.ReciveData = m,
		e.getUrlParam = j,
		e.OpenApiScoreSave = C,
		e.getExpId = D,
		e.getBaseInfo = D,
		e.uploadResultData = N,
		e.uploadFile = E,
		e.downloadData = k,
		e.UploadSteps = S,
		e.ReportEdit = T,
		e.expScoreSave = w,
		e.getUserInfoFn = b,
		e.getUserInfo = x,
		e.uploadFileBase64 = O
	} (window),
	e.OpenApiScoreSave = C,
	e.ReciveData = m,
	e.ReportEdit = T,
	e.UploadSteps = S,
	e.downloadData = k,
	e.expScoreSave = w,
	e.getBaseInfo = D,
	e.getExpId = D,
	e.getUrlParam = j,
	e.getUserInfo = x,
	e.getUserInfoFn = b,
	e.uploadFile = E,
	e.uploadFileBase64 = O,
	e.uploadResultData = N,
	Object.defineProperty(e, "__esModule", {
		value: !0
	})
}));